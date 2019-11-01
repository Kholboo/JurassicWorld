﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanelManager : MonoBehaviour {
    LevelManager levelManager;
    ScoreManager scoreManager;
    public Text levelTxt;
    public Text bestScoreTxt;
    public Text newBestScoreTxt;
    public Text scoreTxt;
    public GameObject score;
    public GameObject newBest;

    void Awake () {
        scoreManager = GetComponentInParent<ScoreManager> ();
        levelManager = GetComponentInParent<LevelManager> ();
    }

    void OnEnable () {
        if (scoreManager.CheckNewBestScore ()) {
            scoreManager.SaveBestScore ();

            newBest.SetActive (true);

            newBestScoreTxt.text = scoreManager.GetBestScore ().ToString ();
        } else {
            score.SetActive (true);

            bestScoreTxt.text = scoreManager.GetBestScore ().ToString ();
            scoreTxt.text = scoreManager.GetScore ().ToString ();
        }

        levelTxt.text = "Level " + levelManager.GetLevel ().ToString ();
        levelManager.SaveLevel ();
    }

    public void OnClickNextBtn () {
        levelManager.LevelUp (true);
        GameManager.Instance.SetState (GameManager.States.Replay);
    }
}