using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : StateMachineBehaviour
{
    public float runSpeed;
    public bool facingRight;
    [HideInInspector]
    public Transform player;
    private Vector3 offset;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0, 225, 0);
        facingRight = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            if (player.transform.position.x < animator.transform.position.x && facingRight)
            {
                facingRight = !facingRight;
                animator.transform.localScale = Vector3.Scale(animator.transform.localScale, new Vector3(-1, 1, 1));
            }
            else if (player.transform.position.x > animator.transform.position.x && !facingRight)
            {
                facingRight = !facingRight;
                animator.transform.localScale = Vector3.Scale(animator.transform.localScale, new Vector3(-1, 1, 1));
            }
            for (int i = -200; i < 200; i += 20)
            {
                offset = new Vector3(i, 225, 0);
            }
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.transform.position + offset, runSpeed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
