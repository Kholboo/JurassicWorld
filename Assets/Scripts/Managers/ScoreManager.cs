using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public void UpdateScore(int score)
    {
        PlayerPrefs.SetInt("SessionScore", GetScore() + score);
    }

    public void ClearScore()
    {
        PlayerPrefs.SetInt("SessionScore", 0);
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt("SessionScore", 0);
    }

    void SaveBestScore()
    {
        PlayerPrefs.SetInt("Score", GetScore());
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("Score", 0);
    }

    public bool CheckBestScore()
    {
        if (GetScore() > GetBestScore())
        {
            SaveBestScore();
            return true;
        }

        return false;
    }

    void OnApplicationQuit()
    {
        ClearScore();
    }
}