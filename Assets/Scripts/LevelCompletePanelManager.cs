﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanelManager : MonoBehaviour {
    LevelManager levelManager;
    GameManager gameManager;
    ScoreManager scoreManager;
    public Text levelTxt;
    public Text bestScoreTxt;
    public Text scoreTxt;
    public GameObject score;
    public GameObject newBest;

    void Awake () {
        GameObject canvas = GameObject.Find ("Canvas");
        gameManager = canvas.GetComponent<GameManager> ();
        scoreManager = canvas.GetComponent<ScoreManager> ();
        levelManager = canvas.GetComponent<LevelManager> ();
    }

    void OnEnable () {
        if (scoreManager.CheckNewBestScore ()) {
            scoreManager.SaveBestScore ();

            newBest.SetActive (true);

            bestScoreTxt.text = scoreManager.GetBestScore ().ToString ();
        } else {
            score.SetActive (true);

            levelTxt.text = "Level " + levelManager.GetLevel ().ToString ();
            scoreTxt.text = scoreManager.GetScore ().ToString ();
        }

        levelManager.SaveLevel ();
    }

    public void OnClickNextBtn () {
        PlayerPrefs.SetInt ("LevelUp", 1);
        gameManager.Replay ();
    }
}