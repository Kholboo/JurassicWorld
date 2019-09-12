using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private enum States { Start, Play, Pause, GameOver }
    PanelManager panelManager;
    States gameState;

    void Start () {
        panelManager = GetComponent<PanelManager> ();
        Application.targetFrameRate = 60;

        if (PlayerPrefs.GetInt ("LevelUp", 0) == 1) {
            panelManager.CloseHomePanel ();
            PlayerPrefs.SetInt ("LevelUp", 0);
            panelManager.OpenHUDPanel ();
        }
    }

    public void Play () {
        gameState = States.Play;
    }

    public void GameOver () {
        gameState = States.GameOver;
    }

    public void Pause () {
        gameState = States.Pause;
    }

    public void Replay () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    public bool IsGameOver () {
        return gameState == States.GameOver;
    }

    public bool IsPlay () {
        return gameState == States.Play;
    }
}