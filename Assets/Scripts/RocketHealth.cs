using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHealth : MonoBehaviour
{

    public float maxHealth = 6;
    public float currentHealth; //aktuelles Leben

    public GameObject explosion;
    public Transform explosionPos;
    public HealthBar healthBar;

    public float damageDuration = 0.3f;
    private Color defaultColor = Color.white;
    public Color damageColor;
    private float damageTime;
    SpriteRenderer sr;

    // Orientation parameters
    [Header("Orientation Parameters")]
    public float safeUpwardAngle = 30f; // The angle in degrees from upright position that is considered "safe"
    public float baseVelocityThreshold = 1.8f; // Base velocity threshold for upright position
    public float sideVelocityThreshold = 1.2f; // Lower threshold for when tilted or upside down

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
        if (collision.gameObject.name is "Tilemap" or "Gegner1" or "Gegner2")
        {
            // Get the current rotation of the rocket
            float currentAngle = GetUpwardAngle();

            // Calculate the appropriate velocity threshold based on the orientation
            float velocityThreshold = CalculateVelocityThreshold(currentAngle);

            // Check if velocity is above the calculated threshold
            if (collision.relativeVelocity.magnitude > velocityThreshold)
            {
                // Calculate damage based on velocity and orientation
                // Damage is the relative velocity magnitude times the orientation multiplier
                float orientationMultiplier = CalculateOrientationDamageMultiplier(currentAngle);
                float damage = collision.relativeVelocity.magnitude * orientationMultiplier;

                // Apply the damage
                TakeDamage(damage);
            }
        }
    }

    // Calculate the angle between the rocket's up direction and the world up direction
    private float GetUpwardAngle()
    {
        // Calculate the angle between the rocket's up vector and the world up vector
        float angle = Vector2.Angle(transform.up, Vector2.up);
        return angle;
    }

    // Calculate velocity threshold based on orientation
    private float CalculateVelocityThreshold(float angle)
    {
        // If within the safe angle, use the base threshold
        if (angle <= safeUpwardAngle)
        {
            return baseVelocityThreshold;
        }
        // Linear interpolation between base and side threshold based on angle
        // 0 to safeUpwardAngle -> baseVelocityThreshold
        // 180 degrees (completely upside down) -> sideVelocityThreshold
        float t = Mathf.InverseLerp(safeUpwardAngle, 180f, angle);
        return Mathf.Lerp(baseVelocityThreshold, sideVelocityThreshold, t);
    }

    // Calculate damage multiplier based on orientation
    private float CalculateOrientationDamageMultiplier(float angle)
    {
        // If nearly upright, normal damage
        if (angle <= safeUpwardAngle)
        {
            return 1.0f;
        }
        // Higher damage multiplier when at bad angles
        // 1.0 at safeUpwardAngle, up to 2.0 when completely upside down
        float multiplier = Mathf.Lerp(1.0f, 2.0f, Mathf.InverseLerp(safeUpwardAngle, 180f, angle));
        return multiplier;
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