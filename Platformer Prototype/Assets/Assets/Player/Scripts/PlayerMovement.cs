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
    public float hangTime = 0.1f;
    private float hangCounter;
    public float jumpBufferLength = 0.05f;
    private float jumpBufferCount;

    //Animations
    public Animator animator;
    public SpriteRenderer playerSR;

    //Camera
    public Transform camTarget;
    public float aheadAmount, aheadSpeed;

    //Attack Point
    public Transform attackPoint;


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
            attackPoint.localPosition = new Vector3(0.181f, 0.067f, 0f);
        }
        else if (horizontalInput < 0f){
            playerSR.flipX = true;
            attackPoint.localPosition = new Vector3(-0.181f, 0.067f, 0f);
        }

        //Jump hangtime timer
        if (IsGrounded()){
            hangCounter = hangTime;
        }
        else{
            hangCounter -= Time.deltaTime;
        }

        //Jump Buffer
        if (Input.GetButtonDown("Jump")){
            jumpBufferCount = jumpBufferLength;
        }
        else{
            jumpBufferCount -= Time.deltaTime;
        }

        //Get input for jumping
        if ((jumpBufferCount >= 0) && (hangCounter > 0f)){
            Jump();
            jumpBufferCount = 0;
            hangCounter = 0;
        }
        else if ((IsGrounded() == false) && (rb.velocity.y < 0)){
            animator.SetInteger("Jump", 2);
            animator.SetBool("Falling", true);
        }
        else if (IsGrounded() && ((animator.GetInteger("Jump") == 2) || (animator.GetInteger("Jump") == 3))){
            animator.SetInteger("Jump", 0);
            animator.SetBool("Falling", false);
        }
        else if (IsGrounded() && (animator.GetInteger("Jump") == 1) && (rb.velocity.y < 0)){
            animator.SetInteger("Jump", 3);
            animator.SetBool("Falling", false);
        }

        //Jump height regulation
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }



        //Camera
        if (Input.GetAxisRaw("Horizontal") != 0){
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        }
    }

    private void FixedUpdate()
    {
        //Horizontal Movement
        rb.velocity = new Vector2(horizontalInput * movSpeed, rb.velocity.y);
    }


    //Player Movement Methods
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //Animation
        animator.SetInteger("Jump", 1);
        animator.SetBool("Combo1", false);
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(groundChecker.position, 0.1f, groundLayers);

        if (groundCheck != null){
            return true;
        }
        return false;
    }

    void FreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void UnFreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
