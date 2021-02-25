using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    //General Variables
    public Transform skeletonTransform;
    public Rigidbody2D rb;
    public Transform playerTransform;
    public RectTransform skeletonHPCanvas;

    //Movement
    public float movSpeed;
    public float followRange;
    public float stopFollowRange;
    public bool isFollowing = false;
    float horizontalMov = 0;
    bool isLookingRight = true;

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
        //Flip Entity
        if ((skeletonTransform.position - playerTransform.position).x <= 0 && animator.GetBool("IsAttacking") == false && isLookingRight == false){
            //Flips gameobject's scale
            Vector3 flipScale = skeletonTransform.localScale;
            flipScale.x *= -1;
            skeletonTransform.localScale = flipScale;

            //Flip Canvas again so we dont flip HP bar
            flipScale = skeletonHPCanvas.localScale;
            flipScale.x *= -1;
            skeletonHPCanvas.localScale = flipScale;

            isLookingRight = true;
        }
        else if ((skeletonTransform.position - playerTransform.position).x > 0 && animator.GetBool("IsAttacking") == false && isLookingRight == true){
            //Flips gameobject's scale
            Vector3 flipScale = skeletonTransform.localScale;
            flipScale.x *= -1;
            skeletonTransform.localScale = flipScale;

            //Flip Canvas again so we dont flip HP bar
            flipScale = skeletonHPCanvas.localScale;
            flipScale.x *= -1;
            skeletonHPCanvas.localScale = flipScale;

            isLookingRight = false;
        }

        //Move entity
        if ((skeletonTransform.position - playerTransform.position).x <= 0 && animator.GetBool("IsAttacking") == false){
            //Sets movement
            horizontalMov = 1;
        }
        else if ((skeletonTransform.position - playerTransform.position).x > 0 && animator.GetBool("IsAttacking") == false){
            //Sets movement
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

     //Stops player from pushing this entity around.
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            FreezeXYMovement();
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.tag == "Player" && animator.GetBool("IsAttacking") == false){
            UnFreezeXYMovement();
        }
    }

    void FreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void UnFreezeXYMovement(){
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    void OnDrawGizmosSelected(){
        if (seeingPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(seeingPoint.position, followRange);
        Gizmos.DrawWireSphere(seeingPoint.position, stopFollowRange);
    }
}
