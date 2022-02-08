using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {
    public GameObject tryAgainText, replayButton;

    void OnEnable () {
        StartCoroutine (WaitAndEnable (0.0f, tryAgainText));
        StartCoroutine (WaitAndEnable (1.0f, replayButton));
    }

    public void Replay () {
        GameManager.Instance.SetState (GameState.Replay);
    }

    IEnumerator WaitAndEnable (float _time, GameObject _gameObject) {
        yield return new WaitForSeconds (_time);
        _gameObject.SetActive (true);
    }
}