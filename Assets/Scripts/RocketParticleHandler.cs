using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketParticleHandler : MonoBehaviour
{

    float particleAnzahl = 0;

    //Komponente
    RocketController controller;


    ParticleSystem particleSystemFire;

    /*emissionModuleFire ist eine Referenz auf das EmissionModule des particleSystemFire,
     * also den Bereich des Partikelsystems, der die Emissionsrate (wie viele Partikel ausgestoßen werden) steuert.
     * Das EmissionModule wird verwendet, um zu kontrollieren, wie viele Partikel pro Sekunde erzeugt werden, 
     * was besonders bei der Anpassung der Partikelmenge nützlich ist.*/

    ParticleSystem.EmissionModule emissionModuleFire;


    private void Awake()
    {
        controller = GetComponent<RocketController>(); //Zugriff auf den RocketController

        particleSystemFire = GetComponent<ParticleSystem>(); //Zugriff auf ParticleSystem 

        //gibt dir Zugriff auf das EmissionModule des Partikelsystems particleSystemFire
        emissionModuleFire = particleSystemFire.emission;

        emissionModuleFire.rateOverTime = 0; //rateOverTime: wie viele Partikel pro Sekunde erzeugt werden. (Am Anfang 0)


    }

    private void Start()
    {
        // Starte die Partikel
        if (particleSystemFire != null)
        {
            particleSystemFire.Play();
        }
    }


    // Update is called once per frame
    void Update()
    {

        //Verringert die particle über zeit
        particleAnzahl = Mathf.Lerp(particleAnzahl, 0, Time.deltaTime * 0.6f);
        emissionModuleFire.rateOverTime = particleAnzahl;

       
    }

    public void PressedBoostButton()
    {
        particleAnzahl = 30;
    }

    public void NotpressedBoostButton()
    {
        particleAnzahl = 0;
    }
}
