using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Mover
{
    protected readonly Rigidbody _rigidbody;
   
    protected readonly PlayerParameters _playerParameters;

    protected Mover()
    {
        _rigidbody = null;
        _playerParameters = null;
    }
    
    protected Mover(Rigidbody rigidbody, PlayerParameters playerParameters)
    {
        _rigidbody = rigidbody;
        _playerParameters = playerParameters;
    }

    public abstract void Tick(float deltaTime);

    protected void ApplyFriction(float deltaTime, float friction)
    {
        var velocity = _rigidbody.velocity;
        // velocity -= velocity.normalized *
        //             (velocity.magnitude * velocity.magnitude * friction * deltaTime);
        var velocityToSubtract = velocity * friction * deltaTime;
        if (velocityToSubtract.magnitude > velocity.magnitude)
        {
            velocityToSubtract = velocityToSubtract.normalized * velocity.magnitude;
        }

        velocity -= velocityToSubtract;
        _rigidbody.velocity = velocity;
    }
}

public class ForceMover : Mover
{
    private readonly InputAction _moveAction;

    public ForceMover() { }
    
    public ForceMover(InputAction moveAction, Rigidbody rigidbody, PlayerParameters playerParameters) 
        : base(rigidbody, playerParameters)
    {
        _moveAction = moveAction;
    }

    private Vector3 getInput
    {
        get
        {
            var input = _moveAction.ReadValue<Vector2>();
            return new Vector3(input.x, 0, input.y);
        }
    }

    public override void Tick(float deltaTime)
    {
        var input = getInput;

        ApplyFriction(deltaTime, friction: _playerParameters.Friction);

        if (input.magnitude > 0.1f)
        {
            var planeForce = _rigidbody.velocity;
            planeForce.y = 0;
            var dot = Vector3.Dot(input, planeForce);
            AddForce(input * _playerParameters.Speed * deltaTime * _playerParameters.SpeedDotMultiplier.Evaluate(dot));
        }
    }
    
    private void AddForce(Vector3 force)
    {
        _rigidbody.velocity += force;
    }
}

public class Charging : Mover
{
    public Charging() { }
    
    public Charging(Rigidbody rigidbody, PlayerParameters playerParameters) 
        : base(rigidbody, playerParameters)
    {
    }

    public override void Tick(float deltaTime)
    {
        if (_rigidbody.velocity.magnitude > 0.1f)
            ApplyFriction(deltaTime, _playerParameters.ChargingFriction);
        else
            _rigidbody.velocity = Vector3.zero;
    }
    
    private void ApplyStopForce(float deltaTime, float stopRate)
    {
        var velocity = _rigidbody.velocity;
        velocity -= velocity.normalized * (velocity.magnitude * stopRate * deltaTime);
        _rigidbody.velocity = velocity;
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
    
    public Dashing(Rigidbody rigidbody, PlayerParameters playerParameters) 
        : base(rigidbody, playerParameters)
    {
        var transform = _rigidbody.transform;
        
        _initialPosition = transform.position;
        _endPosition = _initialPosition + (transform.forward * _playerParameters.DashDistance);

        _timer = 0f;
    }


    public override void Tick(float deltaTime)
    {
        _timer += deltaTime;

        // var t = _playerParameters.DashCurve.Evaluate(_timer / _playerParameters.DashTime);
        // _rigidbody.MovePosition(Vector3.Lerp(_initialPosition, _endPosition, t));

        _rigidbody.velocity = _rigidbody.transform.forward * _playerParameters.DashStartVelocity * 
                              _playerParameters.DashVelocityCurve.Evaluate(_timer / _playerParameters.DashTime);

        if (isDashFinished)
        {
            // Debug.Log($"timer = {_timer}");
            // Debug.Log($"Dashing --- Distance => {(_initialPosition - _rigidbody.transform.position).magnitude} | speed = {_rigidbody.velocity.magnitude}");
        
            OnEndDash?.Invoke();
        }
    }
}

public class Recovering : Mover
{
    public event Action OnEndRecovering;

    private Vector3 _initialPosition;

    public Recovering() {}
    
    public Recovering(Rigidbody rigidbody, PlayerParameters playerParameters) 
        : base(rigidbody, playerParameters)
    {
        // _rigidbody.velocity = Vector3.zero;
        // Debug.Log($"in tangent => {_playerParameters.DashCurve.keys[^1].inTangent} \n on tangent => {_playerParameters.DashCurve.keys[^1].outTangent}");
        // // _rigidbody.velocity = _rigidbody.transform.forward * (_playerParameters.RecoverVelocity / _playerParameters.DashCurve.keys[^1].inTangent);
        // _rigidbody.velocity = _rigidbody.transform.forward * (_playerParameters.DashDistance /
        //                                                       (_playerParameters.DashTime - 1f +
        //                                                        (1f / _playerParameters.DashCurve.keys[^1].inTangent)));

        _initialPosition = _rigidbody.transform.position;
    }

    public override void Tick(float deltaTime)
    {
         ApplyFriction(deltaTime, _playerParameters.RecoveryFriction);
         // if (_rigidbody.velocity.magnitude <= _playerParameters.Speed)
         //     OnEndRecovering?.Invoke();
         
         // _rigidbody.velocity = _rigidbody.transform.forward * (_playerParameters.DashDistance /
         //                                                               (_playerParameters.DashTime - 1f +
         //                                                                (1f / _playerParameters.DashCurve.keys[^1].inTangent)));

         // _rigidbody.velocity -= _rigidbody.velocity * _playerParameters.RecoveryFriction * deltaTime;
        
         // Debug.Log($"Recovering --- Distance => {(_initialPosition - _rigidbody.transform.position).magnitude} | speed = {_rigidbody.velocity.magnitude}");
         if (_rigidbody.velocity.magnitude <= _playerParameters.Speed)
             OnEndRecovering?.Invoke();
    }
}