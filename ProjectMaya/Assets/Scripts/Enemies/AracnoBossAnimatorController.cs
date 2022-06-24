using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AracnoBossAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator aracnoBossUpperGunsAnimator;
    [SerializeField] private Animator aracnoBossLowerGunsAnimator;
    [SerializeField] private Animator aracnoBossBodyAnimator;
    [SerializeField] private Animator aracnoBossLegsAnimator;
    [SerializeField] private Rigidbody2D aracnoBossRigidBody;
    [SerializeField] private Attack aracnoBossAttack;
    [SerializeField] private EnemyStateMachine aracnoBossController;

    private StateMachine stateMachine;
    private IState currentState;
    private WeaponBase selectedWeapon;

    private void Awake() 
    {
        aracnoBossRigidBody = this.GetComponent<Rigidbody2D>();
        aracnoBossAttack = this.GetComponent<Attack>();
        aracnoBossController = this.GetComponent<EnemyStateMachine>();
    }

    private void Start() {
        stateMachine = aracnoBossController.stateMachine;
    }

    private void OnEnable() 
    {
        aracnoBossAttack.OnAttack += TriggerAttackAnimation;
    }

    private void OnDisable() 
    {
        aracnoBossAttack.OnAttack += TriggerAttackAnimation;
    }

    private void Update() 
    {
        GetVelocity();
        GetPhase();
    }

    private void GetVelocity()
    {
        aracnoBossUpperGunsAnimator.SetFloat("velocity", aracnoBossRigidBody.velocity.magnitude);
        aracnoBossLowerGunsAnimator.SetFloat("velocity", aracnoBossRigidBody.velocity.magnitude);
        aracnoBossBodyAnimator.SetFloat("velocity", aracnoBossRigidBody.velocity.magnitude);
        aracnoBossLegsAnimator.SetFloat("velocity", aracnoBossRigidBody.velocity.magnitude);
        aracnoBossLegsAnimator.SetFloat("velocityX", aracnoBossRigidBody.velocity.x);
        aracnoBossLegsAnimator.SetFloat("velocityY", aracnoBossRigidBody.velocity.y);
    }

    private void GetPhase()
    {
        currentState = stateMachine.CurrentState;
        if (currentState is Phase3AracnoState)
        {
            aracnoBossUpperGunsAnimator.SetBool("hover", true);
            aracnoBossLowerGunsAnimator.SetBool("hover", true);
            aracnoBossBodyAnimator.SetBool("hover", true);
            aracnoBossLegsAnimator.SetBool("hover", true);
        }else
        {
            aracnoBossUpperGunsAnimator.SetBool("hover", false);
            aracnoBossLowerGunsAnimator.SetBool("hover", false);
            aracnoBossBodyAnimator.SetBool("hover", false);
            aracnoBossLegsAnimator.SetBool("hover", false);
        }
    }

    private void TriggerAttackAnimation()
    {
        selectedWeapon = aracnoBossAttack.selectedWeapon;
        if (selectedWeapon is BossRocketLauncher)
        {
            aracnoBossUpperGunsAnimator.SetTrigger("shoot");
        }
        if (selectedWeapon is MachineGun)
        {
            aracnoBossLowerGunsAnimator.SetTrigger("shoot");
        }
    }

}
