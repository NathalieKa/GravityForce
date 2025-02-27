using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Author: Korte
//Small script to set the MainMenu HighScore text to its correct Value
//Saved across Sessions
public class HighScoreMainMenu : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;

    void Start()
    {
        if(PlayerPrefs.HasKey("HighScore") == false)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        else
        {
            HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
    }
}
