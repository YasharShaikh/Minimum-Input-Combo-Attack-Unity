using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public static PlayerAnimationHandler instance;
    Animator playerAnimator;
    [HideInInspector] public bool isPerformingSwordAttack = false;
    [HideInInspector] public bool isPerformingEnergyAttack = false;
    private void Awake()
    {
        instance = this;
        playerAnimator = GetComponentInChildren<Animator>();
    }

    #region combat related


    //ISSUE: setting speed of the animation which results in slowing or increasing the speed of overall animation[Movement, Action]
    public void SwordAttack(AttackSO combo)
    {
        isPerformingSwordAttack = true;
        playerAnimator.runtimeAnimatorController = combo.controller;
        playerAnimator.speed = combo.attackSpeed;
        playerAnimator.Play("SwordAttack", 1, 0);
    }
    public void PowerAttack(EnergySO combo)
    {

        isPerformingEnergyAttack = true;
        playerAnimator.runtimeAnimatorController = combo.controller;
        //add control energy attack speed
        playerAnimator.Play("EnergyAttack", 1, 0);
    }
    #endregion

    #region Movement Related
    public void LocomotionAnimaton(float magnitude)
    {
        playerAnimator.SetFloat("velocity", magnitude);
    }

    #endregion

    #region Action related

    public void PerformJumpStart()
    {
        Debug.Log("Performing Jump Animation");
        playerAnimator.SetTrigger("jump");
    }
    public void PerformInAir()
    {
        playerAnimator.SetBool("inAir", true);
    }
    public void PerformJumpEnd()
    {
        playerAnimator.SetBool("inAir", false);
        playerAnimator.SetTrigger("jumpEnd");
    }
    public void rollPerform()
    {
        playerAnimator.SetTrigger("roll");

    }
    #endregion


}
