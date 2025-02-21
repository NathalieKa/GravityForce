using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MenuBackground : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    private Sprite[] backgroundSprites;
    [SerializeField] private float animationDelay = 0.4f;  // Time between frames

    void Start()
    {
        // Load all background sprites
        backgroundSprites = new Sprite[5];  // Adjust array size to match your number of images
        for (int i = 1; i <= 5; i++)
        {
            backgroundSprites[i-1] = Resources.Load<Sprite>("Textures/MainMenu/G" + i);
        }

        // Start the animation loop
        StartCoroutine(AnimateBackground());
    }

    IEnumerator AnimateBackground()
    {
        while (true)  // Infinite loop for continuous animation
        {
            //First Loop animates from 1 to 5
            for (int i = 0; i < backgroundSprites.Length; i++)
            {
                //Debug.Log("Loop 1, current i: " + i);
                backgroundImage.sprite = backgroundSprites[i];
                //Debug.Log($"Showing background image: {i + 1}");
                yield return new WaitForSeconds(animationDelay);
            }
            //Second Loop animates from 5 to 1
            for (int i = (backgroundSprites.Length - 2); i > 0; i--)
            {
                //Debug.Log("Loop 2, current i: " + i);
                backgroundImage.sprite = backgroundSprites[i];
                //Debug.Log($"Showing background image: {i + 1}");
                yield return new WaitForSeconds(animationDelay);
            }
        }
    }
}
