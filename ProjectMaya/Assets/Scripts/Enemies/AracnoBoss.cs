using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AracnoBoss : EnemyStateMachine
{
    LifeSystem lifeSystem;
    protected override void Awake()
    {
        base.Awake();
        lifeSystem = GetComponent<LifeSystem>();

        IdleState idleState = new IdleState(gameObject);
        Phase1AracnoState phase1AracnoState = new Phase1AracnoState(gameObject);
        Phase2AracnoState phase2AracnoState = new Phase2AracnoState(gameObject);
        Phase3AracnoState phase3AracnoState = new Phase3AracnoState(gameObject);

        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, phase1AracnoState, () => targeting.hasTarget && (lifeSystem._currentLife >= lifeSystem._maxLife/2));
        stateMachine.AddTransition(phase1AracnoState, phase2AracnoState, () => targeting.hasTarget && (lifeSystem._currentLife < lifeSystem._maxLife/2));
        stateMachine.AddTransition(phase2AracnoState, phase3AracnoState, () => targeting.hasTarget && (lifeSystem._currentLife < lifeSystem._maxLife/4));
        stateMachine.AddTransition(phase1AracnoState, idleState, () => !targeting.hasTarget);
        stateMachine.AddTransition(phase2AracnoState, idleState, () => !targeting.hasTarget);
        stateMachine.AddTransition(phase3AracnoState, idleState, () => !targeting.hasTarget);
    }
}
