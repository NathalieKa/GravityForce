using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float amp;
    public float freq;

    [SerializeField] Transform[] Points;
    [SerializeField] private float moveSpeed;
    private int pointsIndex = 0;
    private int direction = 1; // 1 = vorwärts, -1 = rückwärts

    // Basisposition, die sich zwischen den Punkten bewegt
    private Vector3 basePos;

    void Start()
    {
        // Startet am ersten Punkt
        basePos = Points[pointsIndex].position;
        transform.position = basePos;
    }

    void Update()
    {
        // Bewegt die Basisposition zum aktuellen Zielpunkt
        basePos = Vector2.MoveTowards(basePos, Points[pointsIndex].position, moveSpeed * Time.deltaTime);

        // Prüft, ob der Zielpunkt fast erreicht wurde
        if (Vector2.Distance(basePos, Points[pointsIndex].position) < 0.01f)
        {
            // Aktualisiere den Index anhand der aktuellen Richtung
            pointsIndex += direction;

            // Wenn der letzte Punkt erreicht wurde, Richtungswechsel einleiten
            if (pointsIndex >= Points.Length)
            {
                // Setze auf den vorletzten Punkt und ändere die Richtung
                pointsIndex = Points.Length - 2;
                direction = -1;
            }
            // Wenn der erste Punkt erreicht wurde, wieder vorwärts fahren
            else if (pointsIndex < 0)
            {
                pointsIndex = 1;
                direction = 1;
            }
        }

        // Berechnet den Sinusoffset (z.B. in Y-Richtung)
        Vector3 sinOffset = new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);

        // Setzt die tatsächliche Position als Summe aus Basisposition und Sinusoffset
        transform.position = basePos + sinOffset;
    }
}
