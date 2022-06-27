using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1AracnoState : IState
{

    private Vector2 movement;
    private readonly EnemyStateMachine ownerController;
    private readonly GameObject ownerGameObject;
    private readonly Rigidbody2D ownerRigidbody;
    private readonly WallChecker wallCheck;
    private readonly Attack attack;
    private readonly float movingSpeed;

    public Phase1AracnoState(GameObject owner)
    {
        ownerGameObject = owner;
        ownerController = owner.GetComponent<EnemyStateMachine>();
        ownerRigidbody = ownerGameObject.GetComponent<Rigidbody2D>();
        wallCheck = ownerGameObject.GetComponent<WallChecker>();
        attack = ownerGameObject.GetComponent<Attack>();
        movingSpeed = ownerController.EnemyParameters.MovingSpeed;
    }

    public void OnEnter()
    {
        Debug.Log("Phase1");
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
        if (!wallCheck.wallAhead)
            Move();
        else
            ownerController.movingRight = ownerController.movingRight ? false : true;
        
        attack.SelectAttack();
    }
    private void Move()
    {
        movement.Set(ownerController.movingRight ? movingSpeed : -movingSpeed, ownerRigidbody.velocity.y);
        ownerRigidbody.velocity = movement;
    }

}
