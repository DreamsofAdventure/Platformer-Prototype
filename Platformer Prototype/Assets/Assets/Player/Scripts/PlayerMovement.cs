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
    public bool isKnockedBack = false;
    public bool isAttacking = false;
    public bool isLookingRight;

    //Rolling
    public float rollSpeed;
    public bool isRolling = false;
    public float rollingCD = 4f;
    public float rollingCDLeft = 0f;

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
    private Vector3 startingCamPosition;

    //Attack Point
    public Transform attackPoint;

    void Start(){
        startingCamPosition = camTarget.localPosition;
    }

    //Update Methods
    private void Update()
    {
        //Get input for movement
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Get input for rolling
        if (Input.GetButtonDown("Fire2") && isKnockedBack == false && isAttacking == false && isRolling == false && rb.velocity.y == 0 && rollingCDLeft <= 0){
            isRolling = true;
            rollingCDLeft = rollingCD;
            Roll();
        }

        if (rollingCDLeft > 0){
            rollingCDLeft -= Time.deltaTime;
        }

        //Animation
        animator.SetFloat("HorSpeed", Mathf.Abs(horizontalInput));

        //Flip Animation
        if (horizontalInput > 0f && isRolling == false){
            playerSR.flipX = false;
            attackPoint.localPosition = new Vector3(0.181f, 0.067f, 0f);
            isLookingRight = true;
        }
        else if (horizontalInput < 0f && isRolling == false){
            playerSR.flipX = true;
            attackPoint.localPosition = new Vector3(-0.181f, 0.067f, 0f);
            isLookingRight = false;
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
        if ((jumpBufferCount >= 0) && (hangCounter > 0f && isKnockedBack == false && isAttacking == false && isRolling == false)){
            FindObjectOfType<AudioManager>().Play("PlayerJump");
            Jump();
            jumpBufferCount = 0;
            hangCounter = 0;
        } //Now, we set the animation
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
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0 && isKnockedBack == false){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }


        //Camera
        if (Input.GetAxisRaw("Horizontal") != 0 && isRolling == false){
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        }
    }

    private void FixedUpdate()
    {
        //Horizontal Movement
        if (isKnockedBack){

        }
        else if (isRolling){
            if (isLookingRight){
                rb.velocity = new Vector2(rollSpeed, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(-rollSpeed, rb.velocity.y);
            }
        }
        else if (isAttacking){
            rb.velocity = new Vector2(0f, 0f);
        }
        else{
            rb.velocity = new Vector2(horizontalInput * movSpeed, rb.velocity.y);
        }


        if (rb.velocity.y > 0){
            camTarget.localPosition = new Vector3(camTarget.localPosition.x, Mathf.Lerp(camTarget.localPosition.y, -0.1f, 1f * Time.deltaTime), camTarget.localPosition.z);
        }
        else if (rb.velocity.y < 0){
            camTarget.localPosition = new Vector3(camTarget.localPosition.x, Mathf.Lerp(camTarget.localPosition.y, 0.2f, 2f * Time.deltaTime), camTarget.localPosition.z);
        }
        else{
            camTarget.localPosition = new Vector3(camTarget.localPosition.x, Mathf.Lerp(camTarget.localPosition.y, startingCamPosition.y, 1f * Time.deltaTime), camTarget.localPosition.z);
        }
    }


    //Player Movement Methods
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //Animation
        animator.SetInteger("Jump", 1);
        animator.SetBool("Combo1", false);
    }

    void Roll(){
        animator.SetBool("IsRolling", true);
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(groundChecker.position, 0.1f, groundLayers);

        if (groundCheck != null){
            return true;
        }
        return false;
    }


    //Animation Events
    void SetIsAttacking(){
        if (isAttacking == false){
            isAttacking = true;
        }
        else{
            isAttacking = false;
        }
    }

    void SetRollingFalse(){
        animator.SetBool("IsRolling", false);
        isRolling = false;
    }

    void FreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void UnFreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
