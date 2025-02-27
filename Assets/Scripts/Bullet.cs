using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Autor: Kascha
    //QUELLE: https://www.youtube.com/watch?v=wkKsl1Mfp5M 

    public float speed = 20f;
    public Rigidbody2D rb;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //Bullet soll nach vorne fliegen
        rb.velocity = transform.up * speed;
        
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }


    //Wenn die Bullet mit etwas collided zum debuggen
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

   
}
