using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private Attack attack;
    private bool bossEnemy;
    private Vector2 bossZoneCornerA;
    private Vector2 bossZoneCornerB;

    private EnemyStateMachine controller;
    private float lookingRange;

    private Vector2 targetingCenter;
    private LayerMask targetingLayerMask;

    public Transform target { get; private set; }
    public bool hasTarget { get; private set; }

    private void Awake()
    {
        controller = GetComponent<EnemyStateMachine>();
        attack = GetComponent<Attack>();
    }

    private void Start()
    {
        bossEnemy = controller.EnemyParameters.IsBoss;
        lookingRange = controller.EnemyParameters.LookingRange;
        targetingCenter = controller.EnemyParameters.TargetingCenter;
        bossZoneCornerA = controller.EnemyParameters.BossZoneCornerA;
        bossZoneCornerB = controller.EnemyParameters.BossZoneCornerB;
        targetingLayerMask = controller.EnemyParameters.TargetingLayerMask;
    }

    private void Update()
    {
        hasTarget = TargetingAction();

        if (hasTarget)
        {
            if (bossEnemy)
            {
                attack.isInRange = true;
            }
            else
            {
                attack.isInRange = !(Vector2.Distance(transform.position, target.position) > attack.attackRange);
            }
        }
    }

    private bool TargetingAction()
    {
        if (bossEnemy)
        {
            // var hitObjectBoss = Physics2D.OverlapArea(bossZoneCornerA, bossZoneCornerB, targetingLayerMask);
            // if (hitObjectBoss != null)
            // {
            //     Debug.Log(hitObjectBoss.gameObject);    
            //     target = CheckHit(hitObjectBoss);
            // }
            // return target != null;
            if (GameManager.isBossStarted)
            {
                target = FindObjectOfType<Player>().gameObject.transform;
            }
            return target != null;
        }

        RaycastHit2D hitObject =
            Physics2D.Raycast(transform.position + new Vector3(targetingCenter.x, targetingCenter.y, 0),
                controller.movingRight ? Vector2.right : Vector2.left, lookingRange, targetingLayerMask);
        target = CheckHit(hitObject);
        return target != null;
    }

    private Transform CheckHit(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            return hit.transform;
        }
        // foreach (Collider2D hit in hits)
        //     if (hit.gameObject.layer == LayerMask.GetMask("Player"))
        //         return hit.transform;
        return null;
    }

    private Transform CheckHit(RaycastHit2D hit)
    {
        if (hit.transform != null)
            if (hit.transform.gameObject.GetComponent<Player>() != null)
                return hit.transform;
        return null;
    }

    protected void OnDrawGizmos()
    {
        // Guard sentence
        if (controller == null)
            return;

        Gizmos.color = Color.yellow;

        if (controller.movingRight)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(lookingRange, 0, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0),
                transform.position + new Vector3(attack.attackRange, 0.5f, 0));
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(-lookingRange, 0, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0),
                transform.position + new Vector3(-attack.attackRange, 0.5f, 0));
        }

        if (bossEnemy)
        {
            Gizmos.DrawLine(new Vector3(bossZoneCornerA.x, bossZoneCornerA.y, 0), new Vector3(bossZoneCornerB.x, bossZoneCornerB.y, 0));
            Gizmos.DrawLine(new Vector3(bossZoneCornerA.x, bossZoneCornerB.y, 0), new Vector3(bossZoneCornerB.x, bossZoneCornerA.y, 0));
            Gizmos.DrawLine(new Vector3(bossZoneCornerA.x, bossZoneCornerA.y, 0), new Vector3(bossZoneCornerB.x, bossZoneCornerA.y, 0));
            Gizmos.DrawLine(new Vector3(bossZoneCornerA.x, bossZoneCornerB.y, 0), new Vector3(bossZoneCornerB.x, bossZoneCornerB.y, 0));
            Gizmos.DrawLine(new Vector3(bossZoneCornerA.x, bossZoneCornerA.y, 0), new Vector3(bossZoneCornerA.x, bossZoneCornerB.y, 0));
            Gizmos.DrawLine(new Vector3(bossZoneCornerB.x, bossZoneCornerA.y, 0), new Vector3(bossZoneCornerB.x, bossZoneCornerB.y, 0));
        }
    }
}
