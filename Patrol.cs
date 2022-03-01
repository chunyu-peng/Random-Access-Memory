using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{
    private GameObject[] patrol;
    int randPoint;
    public float walkSpeed;
    public bool facingRight = false;
    [HideInInspector]
    public Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = GameObject.FindGameObjectsWithTag("PatrolPoint");
        randPoint = Random.Range(0, patrol.Length);
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrol[randPoint].transform.position, walkSpeed * Time.deltaTime);
            if (Vector2.Distance(animator.transform.position, patrol[randPoint].transform.position) < 0.1f)
            {
                randPoint = Random.Range(0, patrol.Length);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
