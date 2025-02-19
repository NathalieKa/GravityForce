using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHealth : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=BLfNP4Sc_iA

    public float maxHealth = 4;
    public float currentHealth; //aktuelles Leben


    public GameObject explosion;
    public Transform explosionPos;
    public HealthBar healthBar;

    public float damageDuration = 0.3f;
    private Color defaultColor = Color.white;
    public Color damageColor;
    private float damageTime;
    SpriteRenderer sr;

    Audiomanager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audiomanager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time >= damageTime){
            sr.color = defaultColor;
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfe, ob die Rakete von einem EnemyBullet 
        if (collision.CompareTag("EnemyBullet"))
        {
            TakeDamage(1); // Schaden von 1 Punkt
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Has to changed to a dynamic way to calculate the damage
        /*
        if (collision.gameObject.name == "Tilemap" || collision.gameObject.name == "Gegner1" || collision.gameObject.name == "Gegner2")
        {
            TakeDamage(1);
        }
        */
        if (collision.gameObject.name == "Tilemap" || collision.gameObject.name == "Gegner1" || collision.gameObject.name == "Gegner2")
        {
            float damage = 0.1f;
            //Dynmically calculate the damage based of the velocity of the object with a cutoff velocity at which to take no damage
            if (collision.relativeVelocity.magnitude > 1.4f)
            {
                damage = collision.relativeVelocity.magnitude;
                TakeDamage(damage);
            }
        }
       
    }

    void TakeDamage(float damage)
    {
        audiomanager.PlayerSFX(audiomanager.damageTaken);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        ApplyDamageColor();
        

        if (currentHealth <= 0)
        {
            Die();
            //When the player dies disable the UI Elements and display the Game Over Screen
            //this function can be called anywhere thanks to the singleton pattern
            GameOverManager.Instance.TriggerGameOver();
        }

    }

    void Die()
    {
        audiomanager.PlayerSFX(audiomanager.death);
        audiomanager.PlayerSFX(audiomanager.gameOver);
        Instantiate(explosion, explosionPos.position, Quaternion.identity); // Explosionsanimation
        Destroy(gameObject); // Rakete zerstören
    }

    void ApplyDamageColor()
    {
        sr.color = damageColor;
        damageTime = Time.time + damageDuration;

    }



}

