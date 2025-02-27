using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Authors: Korte, Kascha

public class EnemyMovement : MonoBehaviour
{
    public float amp;
    public float freq;

    [SerializeField] Transform[] Points;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed; // Geschwindigkeit beim Verfolgen des Spielers
    [SerializeField] private float detectionDistance = 8f; // Erkennungsreichweite wie im Shooting-Script
    [SerializeField] private float maxChaseDistance = 10f; // Maximale Verfolgungsdistanz
    [SerializeField] private float maxChaseTime = 5f; // Maximale Verfolgungszeit in Sekunden
    [SerializeField] private float raycastDistance = 1.0f; // Distanz für Raycast-Prüfungen
    [SerializeField] private float avoidanceForce = 2.0f; // Stärke der Ausweichbewegung
    [SerializeField] private LayerMask obstacleLayer; // Layer für Hindernisse (Tilemap)

    private int pointsIndex;
    private Vector3 basePos; // Basisposition, die sich zwischen den Punkten bewegt
    private GameObject player;
    private bool isChasing = false;
    private float chaseTimer = 0f;
    private Vector3 startChasePos; // Position, an der die Verfolgung begann
    private bool lineOfSight = false;
    private Rigidbody2D rb;

    void Start()
    {
        // Starte an dem ersten Punkt
        basePos = Points[pointsIndex].position;
        transform.position = basePos;
        player = GameObject.FindGameObjectWithTag("Rocket");
        rb = GetComponent<Rigidbody2D>();

        // Falls kein Rigidbody vorhanden ist, fügen wir einen hinzu
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        // Setze obstacle Layer, falls nicht manuell zugewiesen
        if (obstacleLayer == 0)
        {
            obstacleLayer = 1 << LayerMask.NameToLayer("Default");
        }
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

        // Berechne den Sinusoffset (z.B. in Y-Richtung)
        Vector3 sinOffset = new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);

        // Aktualisiere die tatsächliche Position
        transform.position = basePos + sinOffset;
    }

    void FixedUpdate()
    {
        Vector3 movementDirection = Vector3.zero;

        if (isChasing && player != null)
        {
            // Während der Verfolgung
            chaseTimer += Time.deltaTime;

            // Richtung zum Spieler
            movementDirection = (player.transform.position - transform.position).normalized;

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

            // Wende Hindernisvermeidung an
            movementDirection = AvoidObstacles(movementDirection) * chaseSpeed;
        }
        else
        {
            // Normale Patrouillen-Bewegung zwischen Punkten
            movementDirection = (Points[pointsIndex].position - transform.position).normalized;

            // Prüfe, ob der Punkt (nahezu) erreicht wurde
            if (Vector2.Distance(basePos, Points[pointsIndex].position) < 0.01f)
            {
                // Zum nächsten Punkt wechseln (Zyklus wiederholen)
                pointsIndex = (pointsIndex + 1) % Points.Length;
            }

            // Wende Hindernisvermeidung auch im Patrouillenmodus an
            movementDirection = AvoidObstacles(movementDirection) * moveSpeed;
        }

        // Bewege die Basis-Position
        basePos += movementDirection * Time.fixedDeltaTime;
    }

    // Fixing the type mismatch in the AvoidObstacles method
private Vector3 AvoidObstacles(Vector3 currentDirection)
{
    // Erstelle ein Array von Richtungen zum Prüfen
    Vector2[] directions = new Vector2[]
    {
        currentDirection,                                       // Geradeaus
        Quaternion.Euler(0, 0, 45) * currentDirection,         // 45° rechts
        Quaternion.Euler(0, 0, -45) * currentDirection,        // 45° links
        Quaternion.Euler(0, 0, 90) * currentDirection,         // 90° rechts
        Quaternion.Euler(0, 0, -90) * currentDirection,        // 90° links
    };

    // Die resultierende Richtung
    Vector3 resultDirection = currentDirection;

    // Raycasts in verschiedene Richtungen ausführen
    Debug.DrawRay(transform.position, currentDirection * raycastDistance, Color.red);

    for (int i = 0; i < directions.Length; i++)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], raycastDistance, obstacleLayer);
        Debug.DrawRay(transform.position, directions[i] * raycastDistance, Color.blue);

        if (hit.collider != null)
        {
            // Konvertiere transform.position von Vector3 zu Vector2 für die Berechnung
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            Vector2 avoidanceDir = (position2D - hit.point).normalized;

            float weight = 1.0f - (hit.distance / raycastDistance); // Je näher, desto stärker ausweichen

            // Gewichtung nach Richtungsindex - geradeaus hat Priorität
            if (i == 0) weight *= 1.5f;

            // Konvertiere avoidanceDir zurück zu Vector3 für die Addition
            resultDirection += new Vector3(avoidanceDir.x, avoidanceDir.y, 0) * weight * avoidanceForce;
        }
        else if (i == 0) // Wenn kein Hindernis direkt vor uns ist, bevorzuge diese Richtung
        {
            resultDirection += currentDirection * 0.5f;
        }
    }

    // Normalisiere die resultierende Richtung
    return resultDirection.normalized;
}

    // Visuelle Darstellung im Editor
    private void OnDrawGizmosSelected()
    {
        // Zeige den Erkennungsradius an
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        // Zeige die maximale Verfolgungsdistanz an
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxChaseDistance);

        // Zeige die Raycast-Distanz an
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raycastDistance);
    }
}