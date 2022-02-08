using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour {
    public GameObject levelCompleteText, replayButton, levelCollectable;
    public Text totalCoinText, levelCoinText;
    public SpreadCollectable spreadCollectable;
    int levelCoin;

    void OnEnable () {
        totalCoinText.text = GameManager.Instance.coinManager.GetTotalCoin ().ToString ();
        levelCoinText.text = "+" + GameManager.Instance.coinManager.LevelCoin.ToString ();

        StartCoroutine (WaitAndEnable (0.0f, levelCompleteText));
        StartCoroutine (WaitAndEnable (0.5f, levelCollectable));
        StartCoroutine (WaitAndEnable (1.0f, replayButton));

        // GameManager.Instance.levelManager.SaveLevel();
    }

    public void Earn () {
        replayButton.GetComponent<Button> ().interactable = false;

        if (GameManager.Instance.coinManager.LevelCoin > 0) {
            StartCoroutine (SpreadCoins ());
        } else {
            Replay ();
        }
    }

    IEnumerator WaitAndEnable (float _time, GameObject _gameObject) {
        yield return new WaitForSeconds (_time);
        _gameObject.SetActive (true);
    }

    IEnumerator SpreadCoins () {
        yield return new WaitForSeconds (0.2f);

        int coin = GameManager.Instance.coinManager.LevelCoin * GameManager.Instance.coinManager.Multiplier;
        spreadCollectable.Spawn ();

        StartCoroutine (UpdateCoinText (coin));
    }

    IEnumerator UpdateCoinText (int coin) {
        yield return new WaitForSeconds (1.5f);

        int totalCoin = GameManager.Instance.coinManager.GetTotalCoin ();
        GameManager.Instance.coinManager.UpdateCoin (coin, false, true);

        int sub = coin - 25;

        if (coin > 25) {
            totalCoinText.text = (totalCoin + sub).ToString ();
        }

        for (int i = 1; i <= 25; i++) {
            totalCoinText.text = (totalCoin + sub + i).ToString ();
            yield return new WaitForSeconds (0.01f);
        }

        yield return new WaitForSeconds (0.4f);

        Replay ();
    }

    void Replay () {
        GameManager.Instance.SetState (GameState.Replay);
    }

}