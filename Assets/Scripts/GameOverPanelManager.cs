using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelManager : MonoBehaviour {
    GameManager gameManager;
    ScoreManager scoreManager;
    public Text bestScoreTxt;
    public Text scoreTxt;
    public Text newBestScoreTxt;

    void Awake () {
        GameObject canvas = GameObject.Find ("Canvas");
        gameManager = canvas.GetComponent<GameManager> ();
        scoreManager = canvas.GetComponent<ScoreManager> ();
    }

    void OnEnable () {
        if (scoreManager.CheckNewBestScore ()) {
            scoreManager.SaveBestScore ();
        }

        scoreTxt.text = scoreManager.GetScore ().ToString ();
        bestScoreTxt.text = "Best: " + scoreManager.GetBestScore ().ToString ();
    }

    public void OnClickReplayBtn () {
        gameManager.Replay ();
    }
}