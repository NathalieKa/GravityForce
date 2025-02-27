using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    //Autor: Korte

    [SerializeField] public TextMeshProUGUI fuelText;
    [SerializeField] public float fuel = 100f;  //Default Value of 100 but we change this on a level by level basis
    [SerializeField] public float fuelBurnRate = 0.5f;
    private bool boostDown = false;

    private float maxFuel;  //only used to calculate the percentage that gets displayed

    void Awake()
    {
        maxFuel = fuel;
        UpdateFuelDisplay();
    }

    void Update()
    {
        //While the boost button isheld down keep burning fuel
        if (boostDown)
        {
            BurnFuel();
        }
        UpdateFuelDisplay();
    }

    private void UpdateFuelDisplay()
    {
        //Convert to percentage cus thats easier for players to understand
        float fuelPercentage = (fuel / maxFuel) * 100;
        fuelText.text = "Fuel: " + fuelPercentage.ToString("F0") + "%";
    }

    public void setBoostDown()
    {
        boostDown = true;
    }

    public void setBoostUp()
    {
        boostDown = false;
    }

    public void BurnFuel()
    {
        if (fuel > 0)
        {
            fuel -= fuelBurnRate * Time.deltaTime;
            //Time.deltaTime so we dont burn more fuel if weve got a higher framerate
        }
    }
}