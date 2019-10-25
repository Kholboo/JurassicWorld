using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private enum States { Start, Play, Waiting, GameOver, Completed }
    PanelManager panelManager;
    States gameState;

    static GameManager _instance;
    public static GameManager Instance {
        get { return _instance; }
    }

    void Start () {
        _instance = this;
        
        panelManager = GetComponent<PanelManager> ();
        Application.targetFrameRate = 60;

        if (PlayerPrefs.GetInt ("LevelUp", 0) == 1) {
            panelManager.CloseHomePanel ();
            PlayerPrefs.SetInt ("LevelUp", 0);
            Waiting ();
            panelManager.OpenHUDPanel ();
        }
    }

    public void Waiting () {
        gameState = States.Waiting;
    }

    public void Play () {
        gameState = States.Play;
    }

    public void GameOver () {
        gameState = States.GameOver;
    }

    public void Completed () {
        gameState = States.Completed;
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

    public bool IsWaiting () {
        return gameState == States.Waiting;
    }
}