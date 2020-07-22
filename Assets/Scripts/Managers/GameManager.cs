﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public enum States { Start, Play, GameOver, Completed, Replay, Pause }
    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    [EnumToggleButtons]
    public States gameState;
    [HideInInspector]
    public PanelManager panelManager;
    [HideInInspector]
    public LevelManager levelManager;
    [HideInInspector]
    public ScoreManager scoreManager;
    [HideInInspector]
    public CoinManager coinManager;
    [HideInInspector]
    public TapticManager tapticManager;
    [HideInInspector]
    public FileManager fileManager;
    [HideInInspector]
    public EventManager eventManager;

    void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        panelManager = GetComponent<PanelManager>();
        levelManager = GetComponent<LevelManager>();
        scoreManager = GetComponent<ScoreManager>();
        coinManager = GetComponent<CoinManager>();
        tapticManager = GetComponent<TapticManager>();
        fileManager = GetComponent<FileManager>();
        eventManager = GetComponent<EventManager>();

        if (levelManager.IsLevelUp())
        {
            panelManager.ChangeState(Panels.HOMEPANEL, PanelState.CLOSE);

            levelManager.LevelUp(false);
            SetState(States.Start);

            panelManager.ChangeState(Panels.HUDPANEL, PanelState.OPEN);
        }
    }

    public bool CheckState(States state)
    {
        return gameState == state;
    }

    public void SetState(States state)
    {
        if (state == States.Replay)
        {
            Replay();
            return;
        }

        gameState = state;
    }

    void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}