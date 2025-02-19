using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookingAtPlayer : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    public float amp;
    public float freq;
    Vector3 initPos;


    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        // Überprüfe, ob das Ziel noch existiert
        if (target == null)
        {
            // Optional: Skript deaktivieren, falls target zerstört wurde
            enabled = false;
            return;
        }


        float distance = Vector2.Distance(transform.position, target.transform.position);

        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, 0);

        if (distance < 8)
        {

            // Berechne den Richtungsvektor von diesem Objekt zur Rakete
            Vector3 direction = target.position - transform.position;

            // Berechne den Winkel (in Grad) relativ zur x-Achse
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Setze die Rotation um die z-Achse (für 2D-Spiele)
            transform.rotation = Quaternion.AngleAxis(angle + 270f, Vector3.forward);
        }

    }
}
