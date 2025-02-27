using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Author: Korte

public class SettingsMenu : MonoBehaviour
{
    //Default value for motion controls
    //Uses int instead of bool because player prefs dont support bools
    //0 = false
    //1 = true

    public void SetMotionControls()
    {
        Debug.Log("Motion Controls" + PlayerPrefs.GetInt("motionControls"));

        //If motion controls are enabled, disable them
        if (PlayerPrefs.GetInt("motionControls") == 1)
        {
            PlayerPrefs.SetInt("motionControls", 0);
        }
        //If motion controls are disabled# enable them
        else if(PlayerPrefs.GetInt("motionControls") == 0)
        {
            PlayerPrefs.SetInt("motionControls", 1);
        }
        else
        {
            //If no value is set, set it to the default value
            PlayerPrefs.SetInt("motionControls", 1);
        }
        Debug.Log("Motion Controls" + PlayerPrefs.GetInt("motionControls"));
    }
}