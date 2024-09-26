using UnityEngine;



public class PlayerAnimationHandler : MonoBehaviour
{
    public static PlayerAnimationHandler instance;
    Animator playerAnimator;

    private void Awake()
    {
        instance = this;
        playerAnimator = GetComponentInChildren<Animator>();
    }

    #region combat related


    //ISSUE: setting speed of the animation which results in slowing or increasing the speed of overall animation[Movement, Action]
    public void SwordAttack(AttackSO combo)
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f)
        {
            playerAnimator.runtimeAnimatorController = combo.controller;
            playerAnimator.speed = combo.attackSpeed;
            playerAnimator.Play("SwordAttack", 1, 0);
        }
        else
        {
            playerAnimator.speed = combo.attackSpeed;
            Debug.Log("Currently playing combo: " + combo);
        }
    }
    public void PowerAttack(EnergySO combo)
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f)
        {
            playerAnimator.runtimeAnimatorController = combo.controller;
            playerAnimator.Play("EnergyAttack", 1, 0);
        }
        else
        {
            Debug.Log("Currently playing combo: " + combo);
        }

    }
    #endregion

    #region Movement Related
    public void LocomotionAnimaton(float magnitude)
    {
        playerAnimator.SetFloat("velocity", magnitude);
    }

    #endregion

    #region Action related
    public void rollPerform()
    {
        playerAnimator.SetTrigger("roll");

    }
    #endregion


}
