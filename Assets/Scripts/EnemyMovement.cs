using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    //QUELLE:https://www.youtube.com/watch?v=kvQ-QWDWWZI&t=1s

    public float amp;
    public float freq;

    [SerializeField] Transform[] Points;
    [SerializeField] private float moveSpeed;
    private int pointsIndex;

    // Basisposition, die sich zwischen den Punkten bewegt
    private Vector3 basePos;

    void Start()
    {
        // Starte an dem ersten Punkt
        basePos = Points[pointsIndex].position;
        transform.position = basePos;
    }

    void Update()
    {
        // Bewege die Basisposition zu dem aktuellen Zielpunkt
        basePos = Vector2.MoveTowards(basePos, Points[pointsIndex].position, moveSpeed * Time.deltaTime);

        // Prüfe, ob der Punkt (nahezu) erreicht wurde
        if (Vector2.Distance(basePos, Points[pointsIndex].position) < 0.01f)
        {
            // Zum nächsten Punkt wechseln (Zyklus wiederholen)
            pointsIndex = (pointsIndex + 1) % Points.Length;
        }

        // Berechne den Sinusoffset (z.B. in Y-Richtung)
        Vector3 sinOffset = new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);

        // Setze die tatsächliche Position als Summe der Basisposition und des Sinusoffsets
        transform.position = basePos + sinOffset;
    }
}
