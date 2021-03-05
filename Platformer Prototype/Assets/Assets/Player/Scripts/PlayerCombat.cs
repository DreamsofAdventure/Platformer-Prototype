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

    //Jump Variable
    int isJumping;


    void Update()
    {
        isJumping = animator.GetInteger("Jump");

        //Checks if player is jumping to see if the attack will go through or it will stop the combo 
        if (isJumping != 0){
            comboActivated = false;
        }

        //Attack and combo logic
        //Check if combo is activated so that play the animation and calculate the attack
        if (comboActivated && Time.time >= nextAttackTime){
            AttackAnimation();
            nextAttackTime = Time.time + 1f/attackRate;
        }

        //For enabling the ability to combo
        if (Time.time > nextAttackTime && comboEnabled == true){
            comboEnabled = false;
        }

        //Main attack logic
        if (Input.GetButtonDown("Fire1") && isJumping == 0){
            if (Time.time >= nextAttackTime && comboActivated == false && comboEnabled == false){
                AttackAnimation();

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

    void AttackAnimation(){
        //Attack animation
        if (comboActivated == false){
            animator.SetTrigger("Attack1");
        }
        else{
            animator.SetBool("Combo1", true);
        }
    }

    void Attack(){
        //Enemy Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies){
            //TODO Rework enemy script naming
            if(enemy.GetType() == typeof(CircleCollider2D)){
                continue;
            }
            else{
                GameObject enemyGameObject = enemy.gameObject;
                enemyGameObject.SendMessageUpwards("TakeDamage", attackDamage);
            }
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
