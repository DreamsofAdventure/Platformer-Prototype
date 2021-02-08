using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Generic
    public Rigidbody2D rb;
    public Transform groundChecker;
    public LayerMask groundLayers;

    //Moving
    public float movSpeed;
    float horizontalInput;

    //Jumping
    public float jumpForce;

    //Animations
    public Animator animator;
    public SpriteRenderer playerSR;

    //Camera
    public Transform camTarget;
    public float aheadAmount, aheadSpeed;


    //Update Methods
    private void Update()
    {
        //Get input for movement
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Animation
        animator.SetFloat("HorSpeed", Mathf.Abs(horizontalInput));

        //Flip Animation
        if (horizontalInput > 0f){
            playerSR.flipX = false;
        }
        else if (horizontalInput < 0f){
            playerSR.flipX = true;
        }

        //Get input for jumping
        if (Input.GetButtonDown("Jump") && IsGrounded()){
            Jump();
        }
        else if ((IsGrounded() == false) && (rb.velocity.y < 0)){
            animator.SetInteger("Jump", 2);
            animator.SetBool("Falling", true);
        }
        else if (IsGrounded() && (animator.GetInteger("Jump") == 2)){
            animator.SetInteger("Jump", 0);
            animator.SetBool("Falling", false);
        }


        //Camera
        if (Input.GetAxisRaw("Horizontal") != 0){
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalInput * movSpeed, rb.velocity.y);

        rb.velocity = movement;
    }


    //Player Movement Methods
    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;

        //Animation
        animator.SetInteger("Jump", 1);
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayers);

        if (groundCheck != null){
            return true;
        }
        return false;
    }
}
