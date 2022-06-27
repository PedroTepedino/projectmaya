using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] [InlineEditor] 
    private PlayerParameters _playerParameters;

    private Rigidbody2D _Rigidbody2D;

    private Mover _mover;

    public Mover Mover => _mover; // TODO : remove when not necessary

    private PlayerInput _playerInput;
    private int playerID;
    private string currentScheme;

    private void Awake()
    {
        SetupComponents();
    }

    private void SetupComponents()
    {
        _Rigidbody2D = this.GetComponent<Rigidbody2D>();
        _playerInput = this.GetComponent<PlayerInput>();

        _mover = GetMover<ForceMover>();
    }

    private void OnEnable()
    {
        this.transform.DOScale(1, 0.2f).From(0f).SetEase(Ease.OutBack);
        
        _playerInput.actions["Dash"].started += ListenToDashButton;
        _playerInput.actions["Dash"].canceled += ListenToDashButton;
        _playerInput.actions["Pause"].started += ListenOnPause;

    }

    private void OnDisable()
    {
        _playerInput.actions["Dash"].started -= ListenToDashButton;
        _playerInput.actions["Dash"].canceled -= ListenToDashButton;
        _playerInput.actions["Pause"].started -= ListenOnPause;
    }

    private void Update()
    {
        //if (_mover is not Dashing)
            //_aimSystem.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _mover.Tick(Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if (_mover is not Dashing)
        //     return;
        
        // if (collision.gameObject.CompareTag("Player"))
        // {
        //     var other = collision.gameObject.GetComponent<Player>();
        //     HandleCollision(this, other);
        // }
    }


    public void Setup()
    {
        playerID = _playerInput.playerIndex;
        currentScheme = _playerInput.currentControlScheme;

        _playerInput.ActivateInput();
    }

    private void ListenToDashButton(InputAction.CallbackContext context)
    {
        if (_mover is Dashing or Recovering)
            return;
        
        // if (context.started)
        // {
        //     _mover = GetMover<Charging>();
        // }
        if (context.started)
        {
            _mover = GetMover<Dashing>();
            ((Dashing)_mover).OnEndDash += ListenOnDashEnd;
        }
        Debug.Log("dash" + _mover);
    }

    private void ListenOnDashEnd()
    {
        // _mover = GetMover<ForceMover>();
        _mover = GetMover<Recovering>();
        ((Recovering)_mover).OnEndRecovering += ListenOnEndRecovering;
    }

    private void ListenOnPause(InputAction.CallbackContext context)
    {
    }

    private void ListenOnEndRecovering()
    {
        _mover = GetMover<ForceMover>();
    }

    public static void HandleCollision(Player hitter, Player target)
    {
        hitter.SetMover<ForceMover>();
    }

    public void SetMover<T>() where T : Mover, new()
    {
        _mover = GetMover<T>();
    }

    private Mover GetMover<T>() where T : Mover, new()
    {
        T t = new();
        return t switch
        {
            ForceMover => new ForceMover(_playerInput.actions["Move"], _Rigidbody2D, _playerParameters),
            Charging => new Charging(_Rigidbody2D, _playerParameters),
            Dashing => new Dashing(_playerInput.actions["Move"], _Rigidbody2D, _playerParameters),
            Recovering => new Recovering(_Rigidbody2D, _playerParameters),       
            _ => null
        };
    }

    private void OnDrawGizmos()
    {
        if (_playerParameters.CollisionGizmo)
        {
            Gizmos.color = _playerParameters.CollisionGizmoColor;
            Gizmos.DrawWireSphere(this.transform.TransformPoint(this.transform.forward * _playerParameters.CollisionCenterOffset),
                _playerParameters.CollisionRadius);
        }
    }
}
