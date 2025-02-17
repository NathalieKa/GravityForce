using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=wkKsl1Mfp5M


    //referenz zu firePoint Objekt
    public Transform firePoint;
    public GameObject bulletPrefab;

    Cooldown cooldown;

    Audiomanager audiomanager;


    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audiomanager>();
    }


    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        // oder, falls noch nicht vorhanden:
        if (cooldown == null)
        {
            cooldown = gameObject.AddComponent<Cooldown>();
        }
    }
    
    void Update()
    {

      /*  if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }*/
        
    }


    public void ButtonPressed()
    {
        if (!cooldown.isCoolingDown)
        {
            Shoot();
            cooldown.StartCoolDown();
        }
        else
        {
            Debug.Log("Waffe kühlt ab");
        }


    }



    void Shoot()
    {
        //shooting logic
        /*Instantiate() wird verwendet, um eine neue Instanz der bulletPrefab-Kugel zu erzeugen.
        Die Position und Rotation der Kugel wird von firePoint übernommen*/

        //Wir müssen den Bullet dementsprechend rotieren z = 90 in firePoint, weil bei der Erzeugung (Bullet) die Rotation von firepoint übernommen wird. 
        audiomanager.PlayerSFX(audiomanager.shooting);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


    }
}
