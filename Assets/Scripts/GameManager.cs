﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum States { Start, Play, Waiting, GameOver, Completed, Replay }
    static GameManager _instance;
    public static GameManager Instance {
        get { return _instance; }
    }
    States gameState;
    [HideInInspector]
    public PanelManager panelManager;
    [HideInInspector]
    public LevelManager levelManager;
    [HideInInspector]
    public ScoreManager scoreManager;

    void Start () {
        Application.targetFrameRate = 60;

        if (_instance != null && _instance != this) {
            Destroy (gameObject);
            return;
        }

        _instance = this;

        panelManager = GetComponent<PanelManager> ();
        levelManager = GetComponent<LevelManager> ();
        scoreManager = GetComponent<ScoreManager> ();

        if (levelManager.IsLevelUp ()) {
            panelManager.ChangePanelState (PanelManager.Panels.HomePanel, false);

            levelManager.LevelUp (false);
            SetState (States.Waiting);

            panelManager.ChangePanelState (PanelManager.Panels.HUDPanel, true);
        }
    }

    public bool CheckState (States state) {
        return gameState == state;
    }

    public void SetState (States state) {
        if (state == States.Replay) {
            Replay ();
            return;
        }

        gameState = state;
    }

    void Replay () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }
}