using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCPlayerMovement2D : MonoBehaviour
{
    public Animator animator_move;
    public ChapterController2D controller2D;
    public AnimationStateMachine animationState;
    float horizontalMove = 0f;
    public float runSpeed = 80f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator_move.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator_move.SetBool("IsJumping", true);
        }

    }

    public void OnLanding()
    {
        animator_move.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        //Debug.Log("Move player " + (horizontalMove = Input.GetAxis("Horizontal") * runSpeed));
        controller2D.Move(horizontalMove*Time.deltaTime, false, jump);
        jump = false; 
    }
}
