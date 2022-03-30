using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AracnoBoss : EnemyStateMachine
{
    protected override void Awake()
    {
        base.Awake();

        IdleState idleState = new IdleState(gameObject);
        AttackState attackState = new AttackState(gameObject);

        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, attackState, () => targeting.hasTarget);
        stateMachine.AddTransition(attackState, idleState, () => targeting.hasTarget);
    }
}
