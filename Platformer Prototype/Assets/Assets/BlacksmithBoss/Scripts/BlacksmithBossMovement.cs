using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithBossMovement : MonoBehaviour
{
    //General Variables
    public Transform blacksmithTransform;
    public Rigidbody2D rb;
    public Transform playerTransform;
    //public RectTransform blacksmithHPCanvas;

    //Movement
    public float movSpeed;
    public float followRange;
    public float stopFollowRange;
    public bool isFollowing = false;
    float horizontalMov = 0;
    bool isLookingRight = true;

    //Animation
    public Animator animator;

    //Seeing Point & Attack Point
    public Transform seeingPoint;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        //Flip Entity
        if ((blacksmithTransform.position - playerTransform.position).x <= 0 && animator.GetBool("IsAttacking") == false && isLookingRight == false){
            //Flips gameobject's scale
            Vector3 flipScale = blacksmithTransform.localScale;
            flipScale.x *= -1;
            blacksmithTransform.localScale = flipScale;

            //Flip Canvas again so we dont flip HP bar
            //flipScale = blacksmithHPCanvas.localScale;
            //flipScale.x *= -1;
            //blacksmithHPCanvas.localScale = flipScale;

            isLookingRight = true;
        }
        else if ((blacksmithTransform.position - playerTransform.position).x > 0 && animator.GetBool("IsAttacking") == false && isLookingRight == true){
            //Flips gameobject's scale
            Vector3 flipScale = blacksmithTransform.localScale;
            flipScale.x *= -1;
            blacksmithTransform.localScale = flipScale;

            //Flip Canvas again so we dont flip HP bar
            //flipScale = blacksmithHPCanvas.localScale;
            //flipScale.x *= -1;
            //blacksmithHPCanvas.localScale = flipScale;

            isLookingRight = false;
        }

        //Move entity
        if ((blacksmithTransform.position - playerTransform.position).x <= 0 && animator.GetBool("IsSpinning") == false){
            //Sets movement
            horizontalMov = 1;
        }
        else if ((blacksmithTransform.position - playerTransform.position).x > 0 && animator.GetBool("IsSpinning") == false){
            //Sets movement
            horizontalMov = -1;
        }

        //Follow Player
        if (Vector2.Distance(blacksmithTransform.position, playerTransform.position) >= followRange && Vector2.Distance(blacksmithTransform.position, playerTransform.position) < stopFollowRange){
            isFollowing = true;
            Vector2 movement;

            //Adds movement when using spin attack
            if (animator.GetBool("IsSpinning") == true && animator.GetBool("IsAttacking") == true){
                movement = new Vector2(horizontalMov * movSpeed*2, rb.velocity.y);
                rb.velocity = movement;
            }
            else if (animator.GetBool("IsAttacking") == false){
                movement = new Vector2(horizontalMov * movSpeed, rb.velocity.y);
                rb.velocity = movement;
            }
        }
        else{
            //Adds exception from stopping when it is using the spin attack
            if (animator.GetBool("IsSpinning") == true && animator.GetBool("IsAttacking") == true){
                Vector2 movement = new Vector2(horizontalMov * movSpeed*2, rb.velocity.y);
                rb.velocity = movement;
            }
            else{
                rb.velocity = new Vector2(0f, 0f);
                isFollowing = false;
            }
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
        if (collider.gameObject.tag == "Player" && animator.GetBool("IsSpinning") == false){
            FreezeXYMovement();
        }
    }
    void OnTriggerStay2D(Collider2D collider){
        if (collider.gameObject.tag == "Player" && animator.GetBool("IsSpinning") == false){
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
