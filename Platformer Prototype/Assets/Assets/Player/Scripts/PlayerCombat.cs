using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    //Attack Variables
    public float attackRange = 0.75f;
    public int attackDamage = 1;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    //Combo Variables
    public bool comboActivated = false;
    public bool comboEnabled = false;
    float comboTimer = 0f;
    float comboTimerRate = 6f;


    void Update()
    {
        int isJumping = animator.GetInteger("Jump");
        //Attack and combo logic
        if (comboActivated && Time.time >= nextAttackTime){
            Attack();
            nextAttackTime = Time.time + 1f/attackRate;
        }

        if (Time.time > nextAttackTime && comboEnabled == true){
            comboEnabled = false;
        }

        if (Input.GetButtonDown("Fire1") && isJumping == 0){
            if (Time.time >= nextAttackTime && comboActivated == false && comboEnabled == false){
                Attack();

                nextAttackTime = Time.time + 1f/attackRate;
                comboTimer = Time.time + 1f/comboTimerRate;
                comboEnabled = true;
            }
            else if (Time.time < nextAttackTime && Time.time >= comboTimer && comboActivated == false && comboEnabled == true){
                comboActivated = true;
                comboEnabled = false;
            }
        }
    }

    void Attack(){
        //Attack animation
        if (comboActivated == false){
            animator.SetTrigger("Attack1");
        }
        else{
            animator.SetBool("Combo1", true);
        }

        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){
            //TODO Rework enemy script naming
            enemy.GetComponent<SkeletonStats>().TakeDamage(attackDamage);
        }
    }


    //Animation Events
    //Sets combo to false at the end of Attack2 animation
    void SetComboOffAnimation(){
        animator.SetBool("Combo1", false);
        comboActivated = false;
    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
