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


    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfen, ob das Objekt ein Bullet ist
        if (collision.CompareTag("EnemyBullet"))
        {
            TakeDamage(1); //Schaden von 1 Punkt
        }
    }


    void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

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



}

