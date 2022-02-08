using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanel : MonoBehaviour {
    public Text coinText, currentLevelText, nextLevelText;
    public GameObject tip;
    bool hideTip;

    void OnEnable () {
        CoinManager.UpdateCoinText += UpdateCoinText;
        UpdateCoinText ();

        int level = GameManager.Instance.levelManager.GetLevel ();
        currentLevelText.text = level.ToString ();
        nextLevelText.text = (level + 1).ToString ();
    }

    void Update () {
        if (GameManager.Instance.CheckState (GameState.Play)) {
            if (Input.GetMouseButton (0)) {
                if (!hideTip) {
                    hideTip = true;
                    tip.SetActive (false);
                }
            }
        }
    }

    void UpdateCoinText () {
        coinText.text = GameManager.Instance.coinManager.GetTotalCoin ().ToString ();
    }

    void OnDisable () {
        CoinManager.UpdateCoinText -= UpdateCoinText;
    }
}