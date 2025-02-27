using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//Author Korte

public class DebugScript : MonoBehaviour
{
    //This is a DebugScript to find issues with either
    //Toggles, Buttons, Sliders or Dropdowns
    //This script is not supposed to be used for anything else

    //Just drop any of the UI elements in the inspector into the field to find out more
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button _button;
    [SerializeField] private Slider _slider;
    [SerializeField] private Dropdown _dropdown;

    //This function prints out the current state of each UI Element and if its interactable or pressed /used
    public void DebugUI()
    {
        if (_toggle != null)
        {
            Debug.Log("Toggle is on: " + _toggle.isOn);
            Debug.Log("Toggle is interactable: " + _toggle.interactable);
            Debug.Log("Toggle is pressed: " + _toggle.GetComponent<PointerEventData>().button);
        }
        if (_button != null)
        {
            Debug.Log("Button is interactable: " + _button.interactable);
            Debug.Log("Button is pressed: " + _button.GetComponent<PointerEventData>().button);
        }
        if (_slider != null)
        {
            Debug.Log("Slider value: " + _slider.value);
            Debug.Log("Slider is interactable: " + _slider.interactable);
            Debug.Log("Slider is pressed: " + _slider.GetComponent<PointerEventData>().button);
        }
        if (_dropdown != null)
        {
            Debug.Log("Dropdown value: " + _dropdown.value);
            Debug.Log("Dropdown is interactable: " + _dropdown.interactable);
            Debug.Log("Dropdown is pressed: " + _dropdown.GetComponent<PointerEventData>().button);
        }
    }
}
