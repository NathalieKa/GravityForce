using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketInputHandler : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=DVHcOS1E5OQ&list=PLyDa4NP_nvPfmvbC-eqyzdhOXeCyhToko

    //Komponente
    RocketController rocketController;

    private float verticalValue = 0;
    private float horizontalValue = 0;


    /*Es stellt sicher, dass die Referenz (rocketController) verfügbar ist, bevor andere Methoden wie Start() oder Update() sie verwenden.
     * Methode, die bei der Initialisierung eines GameObjects aufgerufen wird.
     */
    private void Awake()
    {
        rocketController = GetComponent<RocketController>();
    }

    // Methode, die den Wert auf 1 setzt (Button gedrückt)
    public void SetVerticalValueToOne()
    {
        verticalValue = 1;
    }

    // Methode, die den Wert auf 0 setzt (Button losgelassen)
    public void SetVerticalValueToZero()
    {
        verticalValue = 0;
    }

    //-1.0 bedeutet eine vollständige Bewegung nach links.
    public void SetHorizobtalValueToMinusOne()
    {
        horizontalValue = -1;
    }

    // Methode, die den Wert auf 0 setzt (Button losgelassen)
    public void SetHorizobtalValueToZero()
    {
        horizontalValue = 0;
    }

    //1.0 bedeutet eine vollständige Bewegung nach rechts.
    public void SetHorizobtalValueToOne()
    {
        horizontalValue = 1;
    }



    // Update is called once per frame
    void Update()
    {

        Vector2 inputVector = Vector2.zero;


        /*Input.GetAxis("Horizontal") gibt einen Wert zwischen -1 und 1 zurück, der die horizontale Eingabe des Spielers repräsentiert.
         * 
         * Tastenbelegung wird im Inputmanager definiert!!!
            
            vertikal:
            -1 bedeutet ganz links (z. B. durch die linke Pfeiltaste oder A-Taste).
            1 bedeutet ganz rechts (z. B. durch die rechte Pfeiltaste oder D-Taste).
            0 bedeutet keine Bewegung (keine Taste gedrückt). */

        //inputVector.x = Input.GetAxis("Horizontal");
        //inputVector.y = Input.GetAxis("Vertical");

        inputVector.x = horizontalValue;
        inputVector.y = verticalValue;



        rocketController.SetInputVector(inputVector);
        
    }
}
