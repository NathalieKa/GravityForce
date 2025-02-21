using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * This class is going to be mostly boilerplate and empty until we actually
 * implement some sort of loading logic or when we start to animate the
 * loading screen with the dots. Until then this will just be a one and a half second timer.
 */
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    private Sprite[] loadingSprites;

    void Start()
    {
        // Load all sprites at start
        loadingSprites = new Sprite[8];
        for (int i = 1; i <= 8; i++)
        {
            loadingSprites[i-1] = Resources.Load<Sprite>("Textures/LoadingScreen/L" + i );
        }
        StartCoroutine(LoadingGame());
    }

    IEnumerator LoadingGame()
    {
        for (int i = 0; i < loadingSprites.Length; i++)
        {
            if (loadingSprites[i] != null)
            {
                loadingImage.sprite = loadingSprites[i];
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                Debug.LogError($"Failed to load sprite L{i+1}");
            }
        }

        int difficulty = PlayerPrefs.GetInt("difficulty");
        SceneManager.LoadScene($"Game{difficulty}");
    }
}