using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelManager : MonoBehaviour {
    GameManager gameManager;

    void Awake () {
        GameObject canvas = GameObject.Find ("Canvas");
        gameManager = canvas.GetComponent<GameManager> ();
    }

    public void OnClickReplayBtn () {
        gameManager.Replay();
    }
}