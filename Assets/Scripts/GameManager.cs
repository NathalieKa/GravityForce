using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int Score;
    private int HighScore;
    public TextMeshProUGUI ScoreText;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("HighScore") == false)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        else
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
        if(PlayerPrefs.HasKey("Score") == false)
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        else
        {
            Score = PlayerPrefs.GetInt("Score");
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreText != null)
        {
            ScoreText.text = "Score: " + Score;
        }
    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DeleteScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
    public void AddScore(int score)
    {
        Score += score;
        if(Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
        PlayerPrefs.SetInt("Score", Score);
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
