using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public PlayerMovement playerMov;

    public int maxHealth = 10;
    int currentHealth;

    float maxStamina;
    float currentStamina;

    //HP UI
    public HealthBar healthBar;
    //Stamina UI
    public StaminaBar staminaBar;
    //Lose UI Elements
    public GameObject endMsg;
    public GameObject endTime;

    //Camera Shake
    public CameraShake cameraShake;
    
    void Start()
    {
        //Set HP
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //Set Stamina
        maxStamina = playerMov.rollingCD;
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    void Update()
    {
        //Updates Stamina levels
        currentStamina = playerMov.rollingCD - playerMov.rollingCDLeft;
        staminaBar.SetStamina(currentStamina);
    }

    public void TakeDamage(int damage){
        //Recieve Damage
        currentHealth -= damage;

        //Set HP UI
        healthBar.SetHealth(currentHealth);

        StartCoroutine(cameraShake.Shake(.15f, .2f));

        //Shine red for visual impact
        StartCoroutine(ShineWhiteCoroutine());

        //Recieve Hit Animation
        animator.SetTrigger("IsHit");

        //If health reaches 0, set Dead state
        if (currentHealth <= 0){
            Death();
        }
        else{
            FindObjectOfType<AudioManager>().Play("PlayerHit");
        }
    }

    IEnumerator ShineWhiteCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    void Death(){
        //Death Animation
        animator.SetBool("IsDead", true);

        //Win UI
        endMsg.SetActive(true);
        endTime.SetActive(true);
        endMsg.GetComponent<EndMsg>().Lose();
        endTime.GetComponent<EndTime>().SetEndTime();

        //Disable Entity
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        this.enabled = false;
    }
}
