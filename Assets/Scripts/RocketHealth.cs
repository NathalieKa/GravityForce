using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHealth : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=BLfNP4Sc_iA

    public int maxHealth = 4;
    public int currentHealth; //aktuelles Leben


    public GameObject explosion;
    public Transform explosionPos;

    public HealthBar healthBar;

    public float damageDuration = 0.3f;
    private Color defaultColor = Color.white;
    public Color damageColor;
    private float damageTime;
    SpriteRenderer sr;


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

        if (collision.gameObject.name == "Tilemap" || collision.gameObject.name == "Gegner1" || collision.gameObject.name == "Gegner2")
        {
            TakeDamage(1);
         
        }   
       
    }

    void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        ApplyDamageColor();

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Instantiate(explosion, explosionPos.position, Quaternion.identity); // Explosionsanimation
        Destroy(gameObject); // Rakete zerstören
    }

    void ApplyDamageColor()
    {
        sr.color = damageColor;
        damageTime = Time.time + damageDuration;

    }



}

