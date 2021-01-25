using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static void Start()
    {
        Bird.Getinstance().OnDied += Bird_OnDied;
    }

    private static void Bird_OnDied(object sender, System.EventArgs e)
    {
        TrySetNewHighscore(Level.GetInstance().GetPipesPassedCount());
    }

    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt("highscore");
    }

    public static bool TrySetNewHighscore(int score)
    {
        int currentHighscore = GetHighscore();
        if (score > currentHighscore)
        {
            //New highscore
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ResetHighscore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
    }
}
