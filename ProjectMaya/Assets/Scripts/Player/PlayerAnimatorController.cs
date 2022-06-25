using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorController : MonoBehaviour
{
   [SerializeField] private Animator playerAnimator;
   [SerializeField] private PlayerInput playerInput;
   [SerializeField] private Rigidbody2D playerRigidbody;
   [SerializeField] private LifeSystem playerLifeSystem;
   [SerializeField] private Player playerController;

   private bool dashing;

   private void Awake() 
   {
        playerInput = this.GetComponent<PlayerInput>();
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        playerAnimator = this.GetComponentInChildren<Animator>();
        playerLifeSystem = this.GetComponent<LifeSystem>();
        playerController = this.GetComponent<Player>();
   }

   private void OnEnable() 
   {
        playerInput.actions["Dash"].canceled += ListenToDashButton;

        playerLifeSystem.OnChangeLife += ListenDamage;
        playerLifeSystem.OnDie += ListenDie;
   }

   private void OnDisable() 
   {
        playerInput.actions["Dash"].canceled -= ListenToDashButton;

        playerLifeSystem.OnChangeLife -= ListenDamage;
        playerLifeSystem.OnDie -= ListenDie;
   }

   private void Update() {
        GetInputDirection();
        GetVelocity();
        GetDashing();
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

   private void GetDashing()
   {
        dashing = (playerController.Mover is Dashing);
        playerAnimator.SetBool("dashing", dashing);
   }

    private void ListenToDashButton(InputAction.CallbackContext context)
    {
        playerAnimator.SetTrigger("dash");
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
