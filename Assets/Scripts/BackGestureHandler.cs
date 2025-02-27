using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackButtonHandler : MonoBehaviour
{
    //Autor Korte

    private bool backPressedOnce = false;
    private float backPressTime = 0f;
    public float backPressThreshold = 2f; // Time window to detect second press in seconds

    // This is a simple back button handler for Android
    // It will load the MainMenu scene when the back button is pressed twice within a short time window
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Android Back Button
        {
            if (backPressedOnce && (Time.time - backPressTime) < backPressThreshold)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                backPressedOnce = true;
                backPressTime = Time.time;
                Debug.Log("Press back again to return to Main Menu");
                StartCoroutine(ResetBackPress());
            }
        }
    }

    IEnumerator ResetBackPress()
    {
        yield return new WaitForSeconds(backPressThreshold);
        backPressedOnce = false;
    }
}