using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithBossCombat : MonoBehaviour
{
    //General Variables
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public Transform blacksmithTransform;
    public Transform playerTransform;

    //Attack Variables
    public float attackRange = 1.85f;
    public float attackPointRange = 1.5f;
    //Triple Attack
    public float attackTripleCD = 3f;
    float nextAttackTripleTime = 0f;
    //Spin Attack
    public float attackSpinCD = 10f;
    float nextAttackSpinTime = 0f;
    //Leap Attack
    public float attackLeapCD = 10f;
    float nextAttackLeapTime = 0f;
    float leapAttackRangeMin = 8.5f;
    float leapAttackRangeMax = 14.5f;
    public Transform leapAttackNewPos; 

    //Attack Points
    public Transform attackPointTriple11;
    public Transform attackPointTriple12;
    public Transform attackPointTriple2;
    public Transform attackPointTriple3;
    public Transform attackPointSpin;
    public Transform attackPointLeap;

    //Colliders
    public Collider2D baseCollider;
    public Collider2D tempCollider1;
    public Collider2D tempCollider2;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Attack Logic
        if (Vector2.Distance(blacksmithTransform.position, playerTransform.position) <= leapAttackRangeMax && Vector2.Distance(blacksmithTransform.position, playerTransform.position) > leapAttackRangeMin && animator.GetBool("IsAttacking") == false){
            if (Time.time >= nextAttackLeapTime){
                //Attack animation
                animator.SetTrigger("AttackLeap");
                nextAttackLeapTime = Time.time + attackLeapCD;
            }
        }
        else if (Vector2.Distance(blacksmithTransform.position, playerTransform.position) <= attackRange && animator.GetBool("IsAttacking") == false){
            if (Time.time >= nextAttackSpinTime){
                //Attack animation
                animator.SetTrigger("AttackSpin");
                nextAttackSpinTime = Time.time + attackSpinCD;
            }
            else if (Time.time >= nextAttackTripleTime){
                //Attack animation
                animator.SetTrigger("AttackTriple");
                nextAttackTripleTime = Time.time + attackTripleCD;
            }
        }
    }




    //All attack functions!
    //Triple Attack
    void AttackTriple1(){
        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointTriple11.position, attackPointTriple12.position, playerLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){

            bool isEnemyRolling = enemy.GetComponent<PlayerMovement>().isRolling;

            if (isEnemyRolling){

            }
            else{
                //Player takes damage
                enemy.GetComponent<PlayerStats>().TakeDamage(1);

                //Knockback
                if ((blacksmithTransform.position - playerTransform.position).x <= 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(2500f, 0f));
                }
                else if ((blacksmithTransform.position - playerTransform.position).x > 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2500f, 0f));
                }
            }
        }
    }
    void AttackTriple2(){
        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointTriple2.position, 2.37f, playerLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){

            bool isEnemyRolling = enemy.GetComponent<PlayerMovement>().isRolling;

            if (isEnemyRolling){

            }
            else{
                //Player takes damage
                enemy.GetComponent<PlayerStats>().TakeDamage(2);

                //Knockback
                if ((blacksmithTransform.position - playerTransform.position).x <= 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(2500f, 0f));
                }
                else if ((blacksmithTransform.position - playerTransform.position).x > 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2500f, 0f));
                }
            }
        }
    }
    void AttackTriple3(){
        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointTriple3.position, 2.8f, playerLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){

            bool isEnemyRolling = enemy.GetComponent<PlayerMovement>().isRolling;

            if (isEnemyRolling){

            }
            else{
                //Player takes damage
                enemy.GetComponent<PlayerStats>().TakeDamage(2);

                //Knockback
                if ((blacksmithTransform.position - playerTransform.position).x <= 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(2500f, 0f));
                }
                else if ((blacksmithTransform.position - playerTransform.position).x > 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2500f, 0f));
                }
            }
        }
    }
    
    //Spin Attack
    void AttackSpin(){
        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointSpin.position, 3f, playerLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){

            bool isEnemyRolling = enemy.GetComponent<PlayerMovement>().isRolling;

            if (isEnemyRolling){

            }
            else{
                //Player takes damage
                enemy.GetComponent<PlayerStats>().TakeDamage(1);

                //Knockback
                if ((blacksmithTransform.position - playerTransform.position).x <= 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(1500f, 0f));
                }
                else if ((blacksmithTransform.position - playerTransform.position).x > 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1500f, 0f));
                }
            }
        }
    }

    //Leap Attack
    void AttackLeap(){
        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeap.position, 3.5f, playerLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){

            bool isEnemyRolling = enemy.GetComponent<PlayerMovement>().isRolling;

            if (isEnemyRolling){

            }
            else{
                //Player takes damage
                enemy.GetComponent<PlayerStats>().TakeDamage(2);

                //Knockback
                if ((attackPointLeap.position - playerTransform.position).x <= 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(7500f, 0f));
                }
                else if ((attackPointLeap.position - playerTransform.position).x > 0){
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-7500f, 0f));
                }
            }
        }
    }

    //Animation Events
    void SetIsAttackingTrue(){
        animator.SetBool("IsAttacking", true);
    }
    void SetIsAttackingFalse(){
        animator.SetBool("IsAttacking", false);
    }
    void SetIsSpinningTrue(){
        animator.SetBool("IsSpinning", true);
    }
    void SetIsSpinningFalse(){
        animator.SetBool("IsSpinning", false);
    }
    //Resets IsHit trigger. This is to use when it starts attacking so that it doesnt save IsHit trigger for after attacking
    void ResetIsHit(){
        animator.ResetTrigger("IsHit");
    }
    void SetNewPositionLeap(){
        this.gameObject.transform.position = leapAttackNewPos.position;
    }
    void SetBaseColliderTrue(){
        baseCollider.enabled = true;
    }
    void SetBaseColliderFalse(){
        baseCollider.enabled = false;
    }
    void SetTempCollider1True(){
        tempCollider1.enabled = true;
    }
    void SetTempCollider1False(){
        tempCollider1.enabled = false;
    }
    void SetTempCollider2True(){
        tempCollider2.enabled = true;
    }
    void SetTempCollider2False(){
        tempCollider2.enabled = false;
    }


    void OnDrawGizmosSelected(){
        if (attackPoint == null){
            return;
        }
        //Gizmos.DrawWireCube(attackPointTriple11.position, boxVector);
        Gizmos.DrawWireSphere(attackPointLeap.position, attackPointRange);



        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //Gizmos.DrawWireSphere(attackPoint.position, leapAttackRangeMin);
        //Gizmos.DrawWireSphere(attackPoint.position, leapAttackRangeMax);
    }
}
