using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float idleTime;
    private readonly float maxIdleTime;
    private float minIdleTime;
    private readonly EnemyStateMachine ownerController;
    private readonly GameObject ownerGameObject;
    private readonly Rigidbody2D ownerRigidbody;
    private float timer;

    public IdleState(GameObject owner)
    {
        ownerGameObject = owner;
        ownerRigidbody = ownerGameObject.GetComponent<Rigidbody2D>();
        ownerController = owner.GetComponent<EnemyStateMachine>();
        maxIdleTime = ownerController.EnemyParameters.MaxIdleTime;
        maxIdleTime = ownerController.EnemyParameters.MinIdleTime;
    }

    public void OnEnter()
    {
        ownerRigidbody.velocity = Vector2.zero;
        timer = 0f;
        idleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    public void OnExit()
    { }

    public void Tick()
    {
        timer += Time.deltaTime;
    }

    public bool TimeEnded()
    {
        return timer > idleTime;
    }
}