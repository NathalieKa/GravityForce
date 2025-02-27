using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Autor: Korte
    public Transform rocket;
    public Vector3 offset;    // Offset to adjust the camera position relative to the rocket


    void LateUpdate()
    {
        //IF clause to stop following the rocket when it dies
        if (!rocket || !rocket.gameObject.activeInHierarchy) return;

        // Follow the rocket's X and Y position while keeping the Z position constant
        Vector3 newPos = new Vector3(rocket.position.x + offset.x, rocket.position.y + offset.y, transform.position.z);
        if (!float.IsNaN(newPos.x) && !float.IsNaN(newPos.y))
        {
            transform.position = newPos;
        } else
        {
            Debug.LogWarning("Calculated camera position contains NaN values.");
        }
    


        // Keep the camera rotation upright (no rotation changes)
        transform.rotation = Quaternion.identity;
    }
}
