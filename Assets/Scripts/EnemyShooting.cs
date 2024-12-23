using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=--u20SaCCow

    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0; // Initialisierung des Timers

        player = GameObject.FindGameObjectWithTag("Rocket");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) // Überprüfen, ob der Player noch existiert
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            Debug.Log(distance);

            if (distance < 10)
            {
                timer += Time.deltaTime; // Timer hochzählen

                if (timer >= 2f) // Prüfen, ob 2 Sekunden vergangen sind
                {
                    timer = 0f; // Timer zurücksetzen
                    shoot();
                }
            }
        }
        else
        {
            Debug.Log("Player wurde zerstört. Schießen wird gestoppt.");
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity); // Klonen des Bullet-Objekts
    }
}