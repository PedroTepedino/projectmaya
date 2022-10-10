using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3AracnoState : IState
{
    private Vector2 movement;
    private readonly EnemyStateMachine ownerController;
    private readonly GameObject ownerGameObject;
    private readonly Rigidbody2D ownerRigidbody;
    private readonly WallChecker wallCheck;
    private readonly Attack attack;
    private Vector3 centralPoint;
    private readonly float movingSpeed;
    private readonly float acceleration = 1.5f;
    private float spiralAngle = 60f;
    private float maxDistanceFromCenter;
    private bool spiralStarted = false;
    private readonly int maxAvalibleWeapons = 11;

    public Phase3AracnoState(GameObject owner)
    {
        ownerGameObject = owner;
        ownerController = owner.GetComponent<EnemyStateMachine>();
        ownerRigidbody = ownerGameObject.GetComponent<Rigidbody2D>();
        wallCheck = ownerGameObject.GetComponent<WallChecker>();
        attack = ownerGameObject.GetComponent<Attack>();
        movingSpeed = ownerController.EnemyParameters.MovingSpeed * acceleration;
        centralPoint = new Vector3(((ownerController.EnemyParameters.BossZoneCornerA.x + ownerController.EnemyParameters.BossZoneCornerB.x) / 2),
                                   ((ownerController.EnemyParameters.BossZoneCornerA.y + ownerController.EnemyParameters.BossZoneCornerB.y) / 2),
                                   0);
        maxDistanceFromCenter = Vector3.Distance(centralPoint, new Vector3(centralPoint.x, ownerController.EnemyParameters.BossZoneCornerA.y, 0)) * 0.5f;
    }

    public void OnEnter()
    {
        spiralStarted = false;
        Debug.Log("Phase3");
        attack.avalibleWeaponsNumber = maxAvalibleWeapons;
    }

    public void OnExit()
    {
        if (!ownerGameObject.GetComponent<Targeting>().hasTarget)
        {
            ownerGameObject.GetComponent<LifeSystem>().Heal(ownerGameObject.GetComponent<LifeSystem>().MaxLife);
        }
    }

    public void Tick()
    {
        if (spiralStarted)
        {
            Move();
        }
        else
        {
            InitialMovement();
        }
        attack.SelectAttack();
    }

    private void Move()
    {
        Vector3 direction = centralPoint - ownerGameObject.transform.position;
        direction = Quaternion.Euler(0, 0, (ownerController.movingRight ? spiralAngle : spiralAngle + 90f)) * direction;
        ownerRigidbody.velocity = direction.normalized * movingSpeed;

        if (Vector3.Distance(centralPoint, ownerGameObject.transform.position) < 0.2f)
        {
            ownerController.movingRight = false;
            Debug.Log(ownerController.movingRight);
        }

        if (Vector3.Distance(centralPoint, ownerGameObject.transform.position) > maxDistanceFromCenter)
        {
            ownerController.movingRight = true;
            Debug.Log(ownerController.movingRight);
        }
    }

    private void InitialMovement()
    {
        var direction = centralPoint - ownerGameObject.transform.position;
        ownerRigidbody.velocity = direction.normalized * movingSpeed;

        if (Vector3.Distance(centralPoint, ownerGameObject.transform.position) < 0.1f)
        {
            spiralStarted = true;
        }
        ownerController.movingRight = false;
    }

}
