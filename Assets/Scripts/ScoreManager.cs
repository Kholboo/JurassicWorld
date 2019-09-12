using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    int score;

    void Awake () {
        score = PlayerPrefs.GetInt ("Score", 0);
    }

    public void UpdateScore (int _score) {
        score += _score;
        PlayerPrefs.SetInt ("Score", score);
    }

    public int GetScore () {
        return score;
    }

    public void SaveBestScore () {
        PlayerPrefs.SetInt ("BestScore", score);
    }

    public int GetBestScore () {
        return PlayerPrefs.GetInt ("BestScore", 0);
    }

    public bool CheckNewBestScore () {
        if (score > PlayerPrefs.GetInt ("BestScore", 0)) {
            return true;
        }
        return false;
    }
}