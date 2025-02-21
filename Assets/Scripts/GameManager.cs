using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Difficulty selection
    // 0 = easy
    // 1 = medium
    // 2 = hard
    public void SelectDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        SwitchScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
