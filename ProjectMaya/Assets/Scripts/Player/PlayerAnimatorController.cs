using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorController : MonoBehaviour
{
   [SerializeField] private Animator playerAnimator;
   [SerializeField] private PlayerInput playerInput;
   [SerializeField] private Rigidbody playerRigidbody;
   [SerializeField] private LifeSystem playerLifeSystem;
   [SerializeField] private SpriteRenderer playerSprite;

   private bool dashing = false;

   private void Awake() 
   {
        playerInput = this.GetComponent<PlayerInput>();
        playerRigidbody = this.GetComponent<Rigidbody>();
        playerAnimator = this.GetComponentInChildren<Animator>();
        playerLifeSystem = this.GetComponent<LifeSystem>();
        playerSprite = this.GetComponent<SpriteRenderer>();
   }

   private void OnEnable() 
   {
        playerInput.actions["Dash"].started += ListenToDashButton;
        playerInput.actions["Dash"].canceled += ListenToDashButton;

        playerLifeSystem.OnChangeLife += ListenDamage;
        playerLifeSystem.OnDie += ListenDie;
   }

   private void OnDisable() 
   {
        playerInput.actions["Dash"].started -= ListenToDashButton;
        playerInput.actions["Dash"].canceled -= ListenToDashButton;

        playerLifeSystem.OnChangeLife -= ListenDamage;
        playerLifeSystem.OnDie -= ListenDie;
   }

   private void Update() {
        GetInputDirection();
        GetVelocity();
        FlipSprite();
        
   }

   private void GetInputDirection()
   {
        var input = playerInput.actions["Move"].ReadValue<Vector2>();
        
        playerAnimator.SetFloat("inputX", input.x);
        playerAnimator.SetFloat("inputY", input.y);
   }

   private void GetVelocity()
   {
        playerAnimator.SetFloat("velocity", playerRigidbody.velocity.magnitude);
   }

   private void GetAimDirection()
   {
        var aim = playerInput.actions["Move"].ReadValue<Vector2>();

        playerAnimator.SetFloat("aimX", aim.x);
        playerAnimator.SetFloat("aimY", aim.y);
   }

    private void ListenToDashButton(InputAction.CallbackContext context)
    {
        if (!dashing)
        {
            playerAnimator.SetTrigger("dash");
            dashing = true;
        }
    }

    public void ListenDashEnd()
    {
        dashing = false;
    }

    private void ListenDamage()
    {
        playerAnimator.SetTrigger("hit");
    }

    private void ListenHeal()
    {
        playerAnimator.SetTrigger("heal");
    }

    private void ListenDie()
    {
        playerAnimator.SetTrigger("die");
    }

    private void FlipSprite()
    {
        if (playerInput.actions["Move"].ReadValue<Vector2>().x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
