using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Author: Korte
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private AudioSource audioSource; // Add reference to AudioSource
    [SerializeField] private AudioClip spriteChangeSound; // Add reference to the sound clip
    private Sprite[] loadingSprites;

    void Start()
    {
        // Load all sprites at start
        loadingSprites = new Sprite[8];
        for (int i = 1; i <= 8; i++)
        {
            loadingSprites[i-1] = Resources.Load<Sprite>("Textures/LoadingScreen/L" + i );
        }

        // Check if AudioSource component exists
        if (audioSource == null)
        {
            // Add AudioSource component if it doesn't exist
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioSource component added automatically");
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

                // Play sound when sprite changes
                if (audioSource != null && spriteChangeSound != null)
                {
                    audioSource.PlayOneShot(spriteChangeSound);
                }
                else if (spriteChangeSound == null)
                {
                    Debug.LogWarning("No sound clip assigned to spriteChangeSound");
                }

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