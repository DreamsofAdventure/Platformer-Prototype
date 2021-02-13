using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    //General Variables
    public Transform skeletonTransform;
    public Rigidbody2D rb;
    public Transform playerTransform;

    //Movement
    public float movSpeed;
    public float followRange;
    public float stopFollowRange;
    public bool isFollowing = false;
    float horizontalMov = 0;

    //Animation
    public Animator animator;
    public SpriteRenderer skeletonSR;

    //Seeing Point & Attack Point
    public Transform seeingPoint;
    public Transform attackPoint;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        //Flip Animation & Attack Point
        if ((skeletonTransform.position - playerTransform.position).x <= 0 && animator.GetBool("IsAttacking") == false){
            skeletonSR.flipX = false;
            attackPoint.localPosition = new Vector3(0.152f, 0f, 0f);
            horizontalMov = 1;
        }
        else if ((skeletonTransform.position - playerTransform.position).x > 0 && animator.GetBool("IsAttacking") == false){
            skeletonSR.flipX = true;
            attackPoint.localPosition = new Vector3(-0.152f, 0f, 0f);
            horizontalMov = -1;
        }

        //Follow Player
        if (Vector2.Distance(skeletonTransform.position, playerTransform.position) >= followRange && Vector2.Distance(skeletonTransform.position, playerTransform.position) < stopFollowRange){
            isFollowing = true;
            //skeletonTransform.position = Vector2.MoveTowards(skeletonTransform.position, playerTransform.position, movSpeed * Time.deltaTime);

            Vector2 movement = new Vector2(horizontalMov * movSpeed, rb.velocity.y);

            rb.velocity = movement;
        }
        else{
            isFollowing = false;
            rb.velocity = new Vector2(0f, 0f);
        }

        //Moving Animation
        if (isFollowing){
            animator.SetBool("IsMoving", true);
        }
        else{
            animator.SetBool("IsMoving", false);
        }
    }

    void OnDrawGizmosSelected(){
        if (seeingPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(seeingPoint.position, followRange);
        Gizmos.DrawWireSphere(seeingPoint.position, stopFollowRange);
    }

    void FreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void UnFreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
}
