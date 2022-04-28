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

    public Phase3AracnoState(GameObject owner)
    {
        ownerGameObject = owner;
        ownerController = owner.GetComponent<EnemyStateMachine>();
        ownerRigidbody = ownerGameObject.GetComponent<Rigidbody2D>();
        wallCheck = ownerGameObject.GetComponent<WallChecker>();
        attack = ownerGameObject.GetComponent<Attack>();
        movingSpeed = ownerController.EnemyParameters.MovingSpeed * acceleration;
        centralPoint = new Vector3(((ownerController.EnemyParameters.BossZoneCornerA.x + ownerController.EnemyParameters.BossZoneCornerB.x)/2), ((ownerController.EnemyParameters.BossZoneCornerA.y + ownerController.EnemyParameters.BossZoneCornerB.y)/2), 0);
        maxDistanceFromCenter = Vector3.Distance(centralPoint, ownerController.EnemyParameters.BossZoneCornerA)*0.75f;
    }

    public void OnEnter()
    {
        spiralStarted = false;
    }

    public void OnExit()
    {
        if (!ownerGameObject.GetComponent<Targeting>().hasTarget)
        {
            ownerGameObject.GetComponent<LifeSystem>().Heal(ownerGameObject.GetComponent<LifeSystem>()._maxLife);
        }
    }

    public void Tick()
    {
        if (spiralStarted)
        {
            Move();     
        }else
        {
            InitialMovement();
        }

        attack.SelectAttack();
    }

    private void Move()
    {
        Vector3 direction = centralPoint - ownerGameObject.transform.position;
        direction = Quaternion.Euler(0, 0, (ownerController.movingRight ? spiralAngle : spiralAngle+90f )) * direction;
        ownerGameObject.transform.Translate(direction.normalized * movingSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(centralPoint, ownerGameObject.transform.position) < 0.1f)
        {
            ownerController.movingRight = false;
        }

        if (Vector3.Distance(centralPoint, ownerGameObject.transform.position) > maxDistanceFromCenter)
        {
            ownerController.movingRight = true;
        }
    }

    private void InitialMovement()
    {
        Vector3 direction = centralPoint - ownerGameObject.transform.position;
        ownerGameObject.transform.Translate(direction.normalized * movingSpeed * Time.deltaTime, Space.World);

        spiralStarted = (Vector3.Distance(centralPoint, ownerGameObject.transform.position) < 0.1f);
        ownerController.movingRight = false;
    }

}
