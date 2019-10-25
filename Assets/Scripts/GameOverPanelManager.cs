using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelManager : MonoBehaviour {
    ScoreManager scoreManager;
    public Text bestScoreTxt;
    public Text scoreTxt;
    public Text newBestScoreTxt;

    void Awake () {
        scoreManager = GetComponentInParent<ScoreManager> ();
    }

    void OnEnable () {
        if (scoreManager.CheckNewBestScore ()) {
            scoreManager.SaveBestScore ();
        }

        scoreTxt.text = scoreManager.GetScore ().ToString ();
        bestScoreTxt.text = "Best: " + scoreManager.GetBestScore ().ToString ();
    }

    public void OnClickReplayBtn () {
        GameManager.Instance.Replay ();
    }
}