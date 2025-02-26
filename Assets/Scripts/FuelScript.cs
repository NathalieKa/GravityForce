using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI fuelText;
    [SerializeField] public float fuel = 100f;
    [SerializeField] public float fuelBurnRate = 0.5f;
    private bool boostDown = false;

    private float maxFuel;

    void Awake()
    {
        maxFuel = fuel;
        UpdateFuelDisplay();
    }

    void Update()
    {
        if (boostDown)
        {
            BurnFuel();
        }
        UpdateFuelDisplay();
    }

    private void UpdateFuelDisplay()
    {
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
        }
    }
}