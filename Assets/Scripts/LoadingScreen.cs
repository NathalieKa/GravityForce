using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * This class is going to be mostly boilerplate and empty until we actually
 * implement some sort of loading logic or when we start to animate the
 * loading screen with the dots. Until then this will just be a one and a half second timer.
 */
public class LoadingScreen : MonoBehaviour
{
    void Start()
    {
        Debug.Log("LoadingScreen Start called");
        StartCoroutine(LoadingGame());
    }

    //Difficulty Easy = 0
    //Difficulty Medium = 1
    //Difficulty Hard = 2
    IEnumerator LoadingGame()
    {
        Debug.Log("Started loading coroutine");
        yield return new WaitForSeconds(1.5f);

        int difficulty = PlayerPrefs.GetInt("difficulty");
        switch (difficulty)
        {
            case 0:
                SceneManager.LoadScene("Game0");
                break;
            case 1:
                SceneManager.LoadScene("Game1");
                break;
            case 2:
                SceneManager.LoadScene("Game2");
                break;

        }
    }
}