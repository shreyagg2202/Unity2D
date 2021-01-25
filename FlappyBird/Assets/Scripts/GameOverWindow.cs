using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;

public class GameOverWindow : MonoBehaviour
{
    private Text scoreText;
    private Text highscoreText;

    private void Awake()
    {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        highscoreText = transform.Find("highscoreText").GetComponent<Text>();
    }

    public void Start()
    {
        Hide();
        Bird.Getinstance().OnDied += Bird_OnDied;
    }

    private void Bird_OnDied(object sender, System.EventArgs e)
    {
        scoreText.text = Level.GetInstance().GetPipesPassedCount().ToString();

        if (Level.GetInstance().GetPipesPassedCount() >= Score.GetHighscore())
        {
            //New highscore
            highscoreText.text = "NEW HIGHSCORE";
        }
        else
        {
            highscoreText.text = "HIGHSCORE: " + Score.GetHighscore();
        }
        Show();
    }

    //Retry button
    public void PlayGame()
    {
        Loader.Loading();
    }

    //Main Menu Button
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
