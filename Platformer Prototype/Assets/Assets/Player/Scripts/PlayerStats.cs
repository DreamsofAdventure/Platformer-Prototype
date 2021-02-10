using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public int maxHealth = 10;
    int currentHealth;
    
    void Start()
    {
        currentHealth= maxHealth;
    }

    public void TakeDamage(int damage){
        //Recieve Damage
        currentHealth -= damage;
        StartCoroutine(ShineWhiteCoroutine());
        //Recieve Hit Animation
        animator.SetTrigger("IsHit");

        //If health reaches 0, set Dead state
        if (currentHealth <= 0){
            Death();
        }
    }

    IEnumerator ShineWhiteCoroutine()
    {
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
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        this.enabled = false;
    }
}
