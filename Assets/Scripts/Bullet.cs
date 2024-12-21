using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=wkKsl1Mfp5M Minute 10 -> Gegner

    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //Bullet soll nach vorne fliegen
        rb.velocity = transform.up * speed;
        
    }


    //Wenn unser Bullet trifft etwas
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

   
}
