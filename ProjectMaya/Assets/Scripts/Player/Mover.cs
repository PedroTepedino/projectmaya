using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Mover
{
    protected readonly Rigidbody2D _Rigidbody2D;
   
    protected readonly PlayerParameters _playerParameters;

    protected Mover()
    {
        _Rigidbody2D = null;
        _playerParameters = null;
    }
    
    protected Mover(Rigidbody2D Rigidbody2D, PlayerParameters playerParameters)
    {
        _Rigidbody2D = Rigidbody2D;
        _playerParameters = playerParameters;
    }

    public abstract void Tick(float deltaTime);

    protected void ApplyFriction(float deltaTime, float friction)
    {
        var velocity = _Rigidbody2D.velocity;
        // velocity -= velocity.normalized *
        //             (velocity.magnitude * velocity.magnitude * friction * deltaTime);
        var velocityToSubtract = velocity * friction * deltaTime;
        if (velocityToSubtract.magnitude > velocity.magnitude)
        {
            velocityToSubtract = velocityToSubtract.normalized * velocity.magnitude;
        }

        velocity -= velocityToSubtract;
        _Rigidbody2D.velocity = velocity;
    }
}

public class ForceMover : Mover
{
    private readonly InputAction _moveAction;

    public ForceMover() { }
    
    public ForceMover(InputAction moveAction, Rigidbody2D Rigidbody2D, PlayerParameters playerParameters) 
        : base(Rigidbody2D, playerParameters)
    {
        _moveAction = moveAction;
    }

    private Vector2 getInput
    {
        get
        {
            return  _moveAction.ReadValue<Vector2>();
        }
    }

    public override void Tick(float deltaTime)
    {
        var input = getInput;

        ApplyFriction(deltaTime, friction: _playerParameters.Friction);

        if (input.magnitude > 0.1f)
        {
            var planeForce = _Rigidbody2D.velocity;
            var dot = Vector2.Dot(input, planeForce);
            AddForce(input * _playerParameters.Speed * deltaTime * _playerParameters.SpeedDotMultiplier.Evaluate(dot));
        }
    }
    
    private void AddForce(Vector2 force)
    {
        _Rigidbody2D.velocity += force/*new Vector2(force.x, force.z)*/;
    }
}

public class Charging : Mover
{
    public Charging() { }
    
    public Charging(Rigidbody2D Rigidbody2D, PlayerParameters playerParameters) 
        : base(Rigidbody2D, playerParameters)
    {
    }

    public override void Tick(float deltaTime)
    {
        if (_Rigidbody2D.velocity.magnitude > 0.1f)
            ApplyFriction(deltaTime, _playerParameters.ChargingFriction);
        else
            _Rigidbody2D.velocity = Vector2.zero;
    }
    
    private void ApplyStopForce(float deltaTime, float stopRate)
    {
        var velocity = _Rigidbody2D.velocity;
        velocity -= velocity.normalized * (velocity.magnitude * stopRate * deltaTime);
        _Rigidbody2D.velocity = velocity;
    }
}

public class Dashing : Mover
{
    private readonly Vector3 _initialPosition;
    private readonly Vector3 _endPosition;
    private float _timer;
    private double _timerD;

    private bool isDashFinished => _timer >= _playerParameters.DashTime;

    public event Action OnEndDash;

    public Dashing() { }
    
    public Dashing(Rigidbody2D Rigidbody2D, PlayerParameters playerParameters) 
        : base(Rigidbody2D, playerParameters)
    {
        var transform = _Rigidbody2D.transform;
        
        _initialPosition = transform.position;
        _endPosition = _initialPosition + (transform.forward * _playerParameters.DashDistance);

        _timer = 0f;
    }


    public override void Tick(float deltaTime)
    {
        _timer += deltaTime;

        // var t = _playerParameters.DashCurve.Evaluate(_timer / _playerParameters.DashTime);
        // _Rigidbody2D.MovePosition(Vector3.Lerp(_initialPosition, _endPosition, t));

        _Rigidbody2D.velocity = _Rigidbody2D.transform.forward * _playerParameters.DashStartVelocity * 
                              _playerParameters.DashVelocityCurve.Evaluate(_timer / _playerParameters.DashTime);

        if (isDashFinished)
        {
            // Debug.Log($"timer = {_timer}");
            // Debug.Log($"Dashing --- Distance => {(_initialPosition - _Rigidbody2D.transform.position).magnitude} | speed = {_Rigidbody2D.velocity.magnitude}");
        
            OnEndDash?.Invoke();
        }
    }
}

public class Recovering : Mover
{
    public event Action OnEndRecovering;

    private Vector3 _initialPosition;

    public Recovering() {}
    
    public Recovering(Rigidbody2D Rigidbody2D, PlayerParameters playerParameters) 
        : base(Rigidbody2D, playerParameters)
    {
        // _Rigidbody2D.velocity = Vector3.zero;
        // Debug.Log($"in tangent => {_playerParameters.DashCurve.keys[^1].inTangent} \n on tangent => {_playerParameters.DashCurve.keys[^1].outTangent}");
        // // _Rigidbody2D.velocity = _Rigidbody2D.transform.forward * (_playerParameters.RecoverVelocity / _playerParameters.DashCurve.keys[^1].inTangent);
        // _Rigidbody2D.velocity = _Rigidbody2D.transform.forward * (_playerParameters.DashDistance /
        //                                                       (_playerParameters.DashTime - 1f +
        //                                                        (1f / _playerParameters.DashCurve.keys[^1].inTangent)));

        _initialPosition = _Rigidbody2D.transform.position;
    }

    public override void Tick(float deltaTime)
    {
         ApplyFriction(deltaTime, _playerParameters.RecoveryFriction);
         // if (_Rigidbody2D.velocity.magnitude <= _playerParameters.Speed)
         //     OnEndRecovering?.Invoke();
         
         // _Rigidbody2D.velocity = _Rigidbody2D.transform.forward * (_playerParameters.DashDistance /
         //                                                               (_playerParameters.DashTime - 1f +
         //                                                                (1f / _playerParameters.DashCurve.keys[^1].inTangent)));

         // _Rigidbody2D.velocity -= _Rigidbody2D.velocity * _playerParameters.RecoveryFriction * deltaTime;
        
         // Debug.Log($"Recovering --- Distance => {(_initialPosition - _Rigidbody2D.transform.position).magnitude} | speed = {_Rigidbody2D.velocity.magnitude}");
         if (_Rigidbody2D.velocity.magnitude <= _playerParameters.Speed)
             OnEndRecovering?.Invoke();
    }
}