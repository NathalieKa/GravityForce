using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject explosion;

    private int maxHealth = 5;


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfen, ob das Objekt ein Bullet ist
        if (collision.CompareTag("RocketBullet"))
        {
            TakeDamage(); //Schaden
        }
    }


    void TakeDamage()
    {
        maxHealth -= 1;

        if (maxHealth <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); // Explosionsanimation
        Destroy(gameObject);

    }
}
