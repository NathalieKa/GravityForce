using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI fuelText;
    [SerializeField]
    public float fuel = 100f;
    [SerializeField]
    private float fuelBurnRate = 0.5f;
    private bool boostDown = false;

    void Update()
    {
        if (boostDown)  // Changed while to if
        {
            BurnFuel();
        }
        fuelText.text = "Fuel: " + fuel.ToString("F0") + "%";
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