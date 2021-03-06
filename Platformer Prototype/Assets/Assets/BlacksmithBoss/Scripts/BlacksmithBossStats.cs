using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithBossStats : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    //HP UI
    //public HealthBar healthBar;
    //public Canvas canvasHP;

    //FeetBoxCollider
    public GameObject feetBoxColl;

    //Audio
    public BlacksmithBossSoundEvents soundEvents;

    //UI Win Elements
    public GameObject endMsg;
    public GameObject endTime;
    public GameObject endMenu;
    
    void Start()
    {
        //Sets starting HP
        currentHealth= maxHealth;

        //Sets starting HP for HP Bar
        //healthBar.SetMaxHealth(maxHealth);

        //Hides the HP canvas
        //healthBar.enabled = false;
    }

    public void TakeDamage(int damage){
        //Recieve Damage
        currentHealth -= damage;

        //Set HP UI
        //healthBar.SetHealth(currentHealth);

        soundEvents.SoundHit();

        //Enables Canvas
        //canvasHP.enabled = true;

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

        //Flashes entity's sprite red to indicate damage
        StartCoroutine(ShineWhiteCoroutine());
    }

    void Death(){
        //Death Animation
        animator.SetBool("IsDead", true);

        //Win UI
        endMsg.SetActive(true);
        endTime.SetActive(true);
        endMsg.GetComponent<EndMsg>().Win();
        endTime.GetComponent<EndTime>().SetEndTime();
        endMenu.SetActive(true);

        //Disable Entity
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<IgnoreCollisionsRoll>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BlacksmithBossMovement>().enabled = false;
        GetComponent<BlacksmithBossCombat>().enabled = false;
        rb.velocity = Vector2.zero;
        Destroy(feetBoxColl);
        //Flashes entity's sprite red to indicate damage
        StartCoroutine(DissapearCoroutine());
    }


    IEnumerator ShineWhiteCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator DissapearCoroutine()
    {
        yield return new WaitForSeconds(1f);
        //canvasHP.enabled = false;
        yield return new WaitForSeconds(60.0f);
        Destroy(gameObject);
    }
}
