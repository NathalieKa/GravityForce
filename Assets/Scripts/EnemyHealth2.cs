using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject explosion;

    private int maxHealth = 6;

    public float damageDuration = 0.3f;
    private Color defaultColor = Color.white;
    private Color damageColor = Color.red;
    private float damageTime;
    SpriteRenderer sr;

    Audiomanager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audiomanager>();
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.time >= damageTime)
        {
            sr.color = defaultColor;
        }
    }

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
        ApplyDamageColor();
        audiomanager.PlayerSFX(audiomanager.damageTaken);

        if (maxHealth <= 0)
        {
            Die();
        }
    }

    void ApplyDamageColor()
    {
        sr.color = damageColor;
        damageTime = Time.time + damageDuration;

    }



    void Die()
    {
        audiomanager.PlayerSFX(audiomanager.death);
        Instantiate(explosion, transform.position, Quaternion.identity); // Explosionsanimation
        Destroy(gameObject);

    }
}
