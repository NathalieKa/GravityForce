using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript1 : MonoBehaviour
{


    // Autor: Kascha
    //QUELLE: https://www.youtube.com/watch?v=--u20SaCCow

    private GameObject player;
    private Rigidbody2D rb;
    public float force;

    // Neuer Winkel-Offset (in Grad), standardmaessig 0
    public float angleOffset = 0f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Rocket");

        // Basisrichtung: vom Schuss zum Spieler
        Vector3 direction = player.transform.position - transform.position;
        // Rotieren der Richtung um einen Offset
        direction = Quaternion.Euler(0, 0, angleOffset) * direction;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotation der Bullet anpassen (optional)
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rocket") || other.gameObject.CompareTag("tilemap"))
        {
            Destroy(gameObject);
        }
    }

}
