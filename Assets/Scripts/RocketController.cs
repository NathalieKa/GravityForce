using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=DVHcOS1E5OQ&list=PLyDa4NP_nvPfmvbC-eqyzdhOXeCyhToko


    [Header("Rocket settins")]
    public float beschleunigungsfaktor = 10.0f; //also wie stark das Objekt beschleunigt, wenn der Benutzer den Befehl zum Beschleunigen gibt.
    public float drehfaktor = 3.5f; //Wie schnell das Objekt sich drehen kann


    //Lokalen Variablen
    float beschleunigungInput = 0;
    float lenkenInput = 0; //Ob nach links oder nach rechts lenken.

    float drehwinkel = 0;

    //Komponenten erkennen
    Rigidbody2D rocketRB;


    /*Die Methode Awake() wird in Unity aufgerufen, sobald das Skript oder GameObject initialisiert wird, also noch bevor das Spiel startet.*/
    private void Awake()
    {
        //Zugriff auf die Komponente
        rocketRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*FixedUpdate() wird in festen Zeitabständen aufgerufen 
     * (standardmäßig alle 0,02 Sekunden oder 50 Mal pro Sekunde), unabhängig von der Bildrate. 
     * Anwendungsbereich: FixedUpdate() ist ideal für Physikberechnungen wie Bewegung, Kräfte und Kollisionen, 
     * da es zu konsistenten und stabilen Ergebnissen führt, unabhängig von der Bildrate.*/
    private void FixedUpdate()
    {
        AddBeschleunigungskraft();

        AddDrehen();
    }

    void AddBeschleunigungskraft()
    {

        //transform.up = Oberseite des Objekts, also in welche Richtung zeigt er. Oben = (0,1)
        Vector2 motorKraftVector = transform.up * beschleunigungsfaktor * beschleunigungInput; //Kraft des Motors / Beispiel: Ergebnis = (0,6)

        //ForceMode2D.Force = langsam, kontinuierlich beschleunigen //ForceMode2D.Impulse = plötzliche Bewegungen
        //AddForce (Kraft zu einem Rigidbody) = Es berücksichtigt die Masse des Objekts. Es berechnet, wie stark das Objekt beschleunigt wird.
        rocketRB.AddForce(motorKraftVector, ForceMode2D.Force);
    }

    void AddDrehen()
    {

        drehwinkel -= lenkenInput * drehfaktor; //wie stark dreht sich die rakete = drehfaktor, lenkeninput = rechts oder links

        rocketRB.MoveRotation(drehwinkel);

    }

    //Hier werden die aktuellen Eingaben von dem Benutzer eingegeben
    public void SetInputVector(Vector2 inputVector)
    {
        lenkenInput = inputVector.x; // -1 bedeutet ganz links , 1 bedeutet ganz rechts, 0 keine bewegung
        beschleunigungInput = inputVector.y;
    }



}
