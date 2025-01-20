using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform rocket;  // Drag your rocket GameObject here in the Inspector
    public Vector3 offset;    // Offset to adjust the camera position relative to the rocket

    void LateUpdate()
    {
        if (!rocket) return;
        // Follow the rocket's X and Y position while keeping the Z position constant
        transform.position = new Vector3(rocket.position.x + offset.x, rocket.position.y + offset.y, transform.position.z);

        // Keep the camera rotation upright (no rotation changes)
        transform.rotation = Quaternion.identity;
    }
}
