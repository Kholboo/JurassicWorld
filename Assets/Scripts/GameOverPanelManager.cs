using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelManager : MonoBehaviour {
    public Text bestScoreTxt;
    public Text scoreTxt;

    void OnEnable () {
        if (GameManager.Instance.scoreManager.CheckNewBestScore ()) {
            GameManager.Instance.scoreManager.SaveBestScore ();
        }

        scoreTxt.text = GameManager.Instance.scoreManager.GetScore ().ToString ();
        bestScoreTxt.text = "Best: " + GameManager.Instance.scoreManager.GetBestScore ().ToString ();

        GameManager.Instance.scoreManager.ClearScore ();
    }

    public void OnClickReplayBtn () {
        GameManager.Instance.SetState (GameManager.States.Replay);
    }
}