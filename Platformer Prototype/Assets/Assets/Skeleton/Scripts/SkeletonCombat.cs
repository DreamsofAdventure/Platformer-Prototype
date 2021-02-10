using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCombat : MonoBehaviour
{
    //General Variables
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public Transform skeletonTransform;
    public Transform playerTransform;

    //Attack Variables
    public float attackRange = 1.85f;
    public float attackPointRange = 1.5f;
    public int attackDamage = 1;
    public float attackCooldown = 2.5f;
    float nextAttackTime = 0f;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        //Attack Logic
        if (Vector2.Distance(skeletonTransform.position, playerTransform.position) <= attackRange){
            if (Time.time >= nextAttackTime){
                //Attack animation
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack(){
        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRange, playerLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<PlayerStats>().TakeDamage(attackDamage);
            if ((skeletonTransform.position - playerTransform.position).x <= 0){
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(5000f, 0f));
            }
            else if ((skeletonTransform.position - playerTransform.position).x > 0){
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5000f, 0f));
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


    void OnDrawGizmosSelected(){
        if (attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRange);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
