using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStats : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public int maxHealth = 5;
    int currentHealth;
    
    void Start()
    {
        currentHealth= maxHealth;
    }

    public void TakeDamage(int damage){
        //TODO Fix player attack times and events in animation for dealing damage
        //so that we dont have to use Coroutines for delaying damage
        StartCoroutine(ShineWhiteCoroutine(damage));
    }

    IEnumerator ShineWhiteCoroutine(int damage)
    {
        yield return new WaitForSeconds(0.1f);
        //Recieve Damage
        currentHealth -= damage;

        //Recieve Hit Animation
        if (animator.GetBool("IsAttacking") == false){
            animator.SetTrigger("IsHit");
        }

        //If health reaches 0, set Dead state
        if (currentHealth <= 0){
            Death();
        }
        else if (currentHealth == 1){
            animator.SetBool("Is1HP", true);
        }

        spriteRenderer.color = Color.red;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    void Death(){
        //Death Animation
        animator.SetBool("IsDead", true);

        //Disable Entity
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SkeletonMovement>().enabled = false;
        GetComponent<SkeletonCombat>().enabled = false;
        this.enabled = false;
    }
}
