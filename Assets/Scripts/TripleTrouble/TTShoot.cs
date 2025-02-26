using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting2 : MonoBehaviour
{
    //QUELLE: https://www.youtube.com/watch?v=--u20SaCCow

    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

    Audiomanager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audiomanager>();
    }



    // Start is called before the first frame update
    void Start()
    {
        timer = 0; // Initialisierung des Timers
        player = GameObject.FindGameObjectWithTag("Rocket");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < 8)
            {
                timer += Time.deltaTime; // Timer hochzählen

                if (timer >= 2f) // Prüfen, ob 2 Sekunden vergangen sind
                {
                    timer = 0f; // Timer zurücksetzen
                    Shoot();
                }
            }
        }
        else
        {
            Debug.Log("Player wurde zerstört. Schießen wird gestoppt.");
        }
    }

    void Shoot()
    {

        audiomanager.PlayerSFX(audiomanager.shooting);

        // Geradeaus
        GameObject bulletCenter = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        bulletCenter.GetComponent<EnemyBulletScript1>().angleOffset = 0f;

        // Nach links (zum Beispiel +15 Grad)
        GameObject bulletLeft = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        bulletLeft.GetComponent<EnemyBulletScript1>().angleOffset = 15f;

        // Nach rechts (zum Beispiel -15 Grad)
        GameObject bulletRight = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        bulletRight.GetComponent<EnemyBulletScript1>().angleOffset = -15f;
    }
}
