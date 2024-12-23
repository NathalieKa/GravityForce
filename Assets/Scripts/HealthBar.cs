using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //store slider in a variable

public class HealthBar : MonoBehaviour
{

    //QUELLE: https://www.youtube.com/watch?v=BLfNP4Sc_iA

    public Slider slider;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health; //aktueller Wert
    }

   
}
