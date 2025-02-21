using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if(PlayerPrefs.GetInt("motionControls")!=0 && PlayerPrefs.GetInt("motionControls")!=1)
        {
            PlayerPrefs.SetInt("motionControls", 1);
        }
        if (PlayerPrefs.GetInt("difficulty")!>=0 && PlayerPrefs.GetInt("difficulty")!<=2)
        {
            PlayerPrefs.SetInt("difficulty", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
