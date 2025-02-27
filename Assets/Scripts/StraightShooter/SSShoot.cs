using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Authors: Korte, Kascha

public class SSShoot : MonoBehaviour
{
    //This class was initally just about shooting at the player if its in range
    //But we added a line of sight check to make the enemy more intelligent and dont shoot at walls
    //The enemy will only shoot if the player is in range and in line of sight
    //We achieved this by casting a line from the enemy to the player and checking if the player is hit and a layermask is used to only check for the player and the map

    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;
    private bool lineOfSight = false;

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
        timer += Time.deltaTime; //Cooldown des Schusses

        if (player != null) // Ueberpruefen, ob der Player noch existiert
        {
            //a layer mask is used to only check for collisions with the player and the map
            //The bit shift operator is used to move the 1 in binary to the 6th position
            //I think this is similar to the masks in networking
            int layerMask = 1 << 6;

            //Check if the enemy has line of sight to the player
            //This is done by casting a line from the enemy to the player
            RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position,layerMask);

            //Compute the distance between the enemy and the player
            float distance = Vector2.Distance(transform.position, player.transform.position);
            //Debug.Log(distance);

            //The distance is abitrary and we mostly decided on 8 due to the size of the players view
            if(hit.collider != null && hit.collider.gameObject.CompareTag("Rocket"))
            {
                lineOfSight = true;
                //Debug.Log("Distance: " + distance + " Line of Sight: " + lineOfSight + "Hit Gameobject: " + hit.collider.gameObject.name);

            }
            else
            {
                lineOfSight = false;
                //Debug.Log("Distance: " + distance + " Line of Sight: " + lineOfSight);

            }

            if (distance < 8&&lineOfSight==true)
            {
                if(timer>=2.0f)
                {
                    shoot();
                    timer = 0;
                }
            }
        }
        else
        {
            Debug.Log("Player wurde zerstoert. Schiessen wird gestoppt.");

            //Disable the script to stop the enemy from shooting and stop thousands of debug.logs
            this.enabled = false;
        }
    }

    void shoot()
    {
        audiomanager.PlayerSFX(audiomanager.shooting);
        Instantiate(bullet, bulletPos.position, Quaternion.identity); // Klonen des Bullet-Objekts

    }
}