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

    IEnumerator LoadingGame()
    {
        Debug.Log("Started loading coroutine");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("TEST");

        try
        {
            SceneManager.LoadScene("Game");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene: {e.Message}");
        }
    }
}