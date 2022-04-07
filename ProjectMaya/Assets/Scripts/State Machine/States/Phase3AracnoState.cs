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
    private readonly float movingSpeed;
    private readonly float acceleration = 1.5f;

    public Phase3AracnoState(GameObject owner)
    {
        ownerGameObject = owner;
        ownerController = owner.GetComponent<EnemyStateMachine>();
        ownerRigidbody = ownerGameObject.GetComponent<Rigidbody2D>();
        wallCheck = ownerGameObject.GetComponent<WallChecker>();
        attack = ownerGameObject.GetComponent<Attack>();
        movingSpeed = ownerController.EnemyParameters.MovingSpeed*acceleration;
    }

    public void OnEnter()
    {
        
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
        attack.SelectAttack();
    }

    private void Move()
    {

    }

}
