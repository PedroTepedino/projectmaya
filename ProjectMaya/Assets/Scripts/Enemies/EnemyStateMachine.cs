using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class EnemyStateMachine : MonoBehaviour
{
    public EnemyParameters EnemyParameters => _enemyParameters;
    public StateMachine stateMachine;
    public bool movingRight = true;
    // public bool alive = true;

    [SerializeField][AssetsOnly][InlineEditor] protected EnemyParameters _enemyParameters;
    protected Attack attack;
    // protected int healthPoints;
    protected Rigidbody2D enemyRigidbody;
    protected Targeting targeting;

    protected virtual void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        targeting = GetComponent<Targeting>();
        attack = GetComponent<Attack>();
        // healthPoints = EnemyParameters.MaxHealthPoints;

        stateMachine = new StateMachine();
    }

    protected virtual void Update()
    {
        stateMachine.Tick();
    }

    protected virtual void OnCollisionEnter2D(Collision2D hit)
    {
        
    }
}
