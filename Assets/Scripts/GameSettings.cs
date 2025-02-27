using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Korte
//This script doesnt get used anywhere is deprecated
//Kept for referemce
public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public bool motionControl = true; // Default value


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Enforce singleton pattern
        }
    }
}
