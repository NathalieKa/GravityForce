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
    [SerializeField] private float chaseSpeed; // Geschwindigkeit beim Verfolgen des Spielers
    [SerializeField] private float detectionDistance = 8f; // Erkennungsreichweite wie im Shooting-Script
    [SerializeField] private float maxChaseDistance = 10f; // Maximale Verfolgungsdistanz
    [SerializeField] private float maxChaseTime = 5f; // Maximale Verfolgungszeit in Sekunden

    private int pointsIndex;
    private Vector3 basePos; // Basisposition, die sich zwischen den Punkten bewegt
    private GameObject player;
    private bool isChasing = false;
    private float chaseTimer = 0f;
    private Vector3 startChasePos; // Position, an der die Verfolgung begann
    private bool lineOfSight = false;

    void Start()
    {
        // Starte an dem ersten Punkt
        basePos = Points[pointsIndex].position;
        transform.position = basePos;
        player = GameObject.FindGameObjectWithTag("Rocket");
    }

    void Update()
    {
        if (player != null)
        {
            // Überprüfe Sichtlinie zum Spieler (ähnlich wie im Shooting-Script)
            int layerMask = 1 << 6;
            RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, layerMask);

            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Prüfe, ob der Spieler in Sichtlinie ist
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Rocket"))
            {
                lineOfSight = true;
            }
            else
            {
                lineOfSight = false;
            }

            // Entscheide, ob der Feind den Spieler verfolgen soll
            if (distanceToPlayer < detectionDistance && lineOfSight && !isChasing)
            {
                // Starte Verfolgung
                isChasing = true;
                chaseTimer = 0f;
                startChasePos = transform.position;
            }
        }

        if (isChasing && player != null)
        {
            // Während der Verfolgung
            chaseTimer += Time.deltaTime;

            // Bewege direkt zum Spieler
            Vector3 direction = (player.transform.position - transform.position).normalized;
            basePos += direction * chaseSpeed * Time.deltaTime;

            // Beende die Verfolgung, wenn die maximale Zeit oder Distanz erreicht ist
            float chasedDistance = Vector3.Distance(startChasePos, transform.position);
            if (chaseTimer >= maxChaseTime || chasedDistance >= maxChaseDistance || !lineOfSight)
            {
                isChasing = false;
                // Setze auf den nächsten Punkt zurück, wenn die Verfolgung endet
                float minDist = float.MaxValue;
                int closestPointIndex = 0;

                // Finde den nächsten Punkt
                for (int i = 0; i < Points.Length; i++)
                {
                    float dist = Vector2.Distance(transform.position, Points[i].position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closestPointIndex = i;
                    }
                }

                pointsIndex = closestPointIndex;
            }
        }
        else
        {
            // Normale Patrouillen-Bewegung zwischen Punkten
            basePos = Vector2.MoveTowards(basePos, Points[pointsIndex].position, moveSpeed * Time.deltaTime);

            // Prüfe, ob der Punkt (nahezu) erreicht wurde
            if (Vector2.Distance(basePos, Points[pointsIndex].position) < 0.01f)
            {
                // Zum nächsten Punkt wechseln (Zyklus wiederholen)
                pointsIndex = (pointsIndex + 1) % Points.Length;
            }
        }

        // Berechne den Sinusoffset (z.B. in Y-Richtung)
        Vector3 sinOffset = new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);

        // Setze die tatsächliche Position als Summe der Basisposition und des Sinusoffsets
        transform.position = basePos + sinOffset;
    }

    // Optional: Visuelle Darstellung im Editor
    private void OnDrawGizmosSelected()
    {
        // Zeige den Erkennungsradius an
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        // Zeige die maximale Verfolgungsdistanz an
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxChaseDistance);
    }
}