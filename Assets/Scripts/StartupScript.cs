using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Korte
public class StartupScript : MonoBehaviour
{
    // Makes sure the game is in the correct orientation and sets the default values for the motioncontrols
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if(PlayerPrefs.GetInt("motionControls")!=0 && PlayerPrefs.GetInt("motionControls")!=1)
        {
            PlayerPrefs.SetInt("motionControls", 1);
        }
    }
}
