using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyResetFlag : StateMachineBehaviour
{
    PlayerAnimationHandler playerAnimationHandler;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerAnimationHandler == null)
        {
            playerAnimationHandler = animator.GetComponentInParent<PlayerAnimationHandler>();
        }

        // Reset the isPerformingSwordAttack flag regardless of whether it's the first time or not.
        if (playerAnimationHandler != null)
        {
            playerAnimationHandler.isPerformingEnergyAttack = false;
        }
        else
        {
            Debug.LogError("PlayerCombatHandler not found on parent object!");
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
