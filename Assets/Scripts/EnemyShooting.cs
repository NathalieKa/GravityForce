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
        if (player != null) // �berpr�fen, ob der Player noch existiert
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            //Debug.Log(distance);

            if (distance < 8)
            {
                timer += Time.deltaTime; // Timer hochz�hlen

                if (timer >= 2f) // Pr�fen, ob 2 Sekunden vergangen sind
                {
                    timer = 0f; // Timer zur�cksetzen
                    shoot();
                }
            }
        }
        else
        {
            Debug.Log("Player wurde zerst�rt. Schie�en wird gestoppt.");
        }
    }

    void shoot()
    {
        audiomanager.PlayerSFX(audiomanager.shooting);
        Instantiate(bullet, bulletPos.position, Quaternion.identity); // Klonen des Bullet-Objekts

    }
}