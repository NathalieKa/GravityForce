using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=wkKsl1Mfp5M


    //reference to firePoint Object
    public Transform firePoint;

    public GameObject bulletPrefab;

    
    void Update()
    {

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        
    }

    void Shoot()
    {
        //shooting logic
        /*Instantiate() wird verwendet, um eine neue Instanz der bulletPrefab-Kugel zu erzeugen.
        Die Position und Rotation der Kugel wird von firePoint übernommen*/

        //Wir müssen den Bullet dementsprechend rotieren z = 90 in firePoint, weil bei der Erzeugung (Bullet) die Rotation von firepoint übernommen wird. 
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
