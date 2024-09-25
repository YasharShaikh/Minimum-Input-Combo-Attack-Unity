using player;
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

    public void swordAttack(AttackSO combo)
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f )
        {
            playerAnimator.runtimeAnimatorController = combo.controller;
            playerAnimator.Play("SwordAttack", 1, 0);
        }
        else
        {
            Debug.Log("Currently playing combo: " + combo);
        }
    }
    public void powerAttack(EnergySO combo)
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime>0.5f )
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


    bool AnimatorIsPlaying()
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(1).length > playerAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime;
    }
    bool AnimatorIsPlayingOnLayer(int layerIndex)
    {
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(layerIndex);
        return stateInfo.length > stateInfo.normalizedTime;  // True if animation is still playing
    }
}
