using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_ani : StateMachineBehaviour
{
    int count = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        count = 0;
        animator.gameObject.GetComponent<Sword_player>().attackCollider.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        count++;
        if (count == 3)
        {
            animator.gameObject.transform.localScale = Vector3.one;
            if (animator.gameObject.GetComponent<Sword_player>().left)
            {
                animator.gameObject.transform.GetChild(0).transform.localScale= Vector3.one - 2 * Vector3.up;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<Sword_player>().left)
        {
            animator.gameObject.transform.localScale = Vector3.one - 2 * Vector3.right;
            animator.gameObject.transform.GetChild(0).transform.localScale = Vector3.one;
        }
        animator.SetBool("attack", false);
        animator.gameObject.GetComponent<Sword_player>().attackCollider.SetActive(false);
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
