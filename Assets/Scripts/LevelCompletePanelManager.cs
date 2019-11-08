using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanelManager : MonoBehaviour {
    public Text levelTxt;
    public Text bestScoreTxt;
    public Text newBestScoreTxt;
    public Text scoreTxt;
    public GameObject score;
    public GameObject newBest;

    void OnEnable () {
        if (GameManager.Instance.scoreManager.CheckNewBestScore ()) {
            GameManager.Instance.scoreManager.SaveBestScore ();

            newBest.SetActive (true);

            newBestScoreTxt.text = GameManager.Instance.scoreManager.GetBestScore ().ToString ();
        } else {
            score.SetActive (true);

            bestScoreTxt.text = "Best: " + GameManager.Instance.scoreManager.GetBestScore ().ToString ();
            scoreTxt.text = GameManager.Instance.scoreManager.GetScore ().ToString ();
        }

        levelTxt.text = "Level " + GameManager.Instance.levelManager.GetLevel ().ToString ();
        GameManager.Instance.levelManager.SaveLevel ();
    }

    public void OnClickNextBtn () {
        GameManager.Instance.levelManager.LevelUp (true);
        GameManager.Instance.SetState (GameManager.States.Replay);
    }
}