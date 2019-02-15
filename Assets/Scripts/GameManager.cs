﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum States { Start, Play, Pause, GameOver };

    States gameState;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        gameState = States.Play;
    }

    public void GameOver()
    {
        gameState = States.GameOver;
    }

    public void Pause()
    {
        gameState = States.Pause;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsGameOver()
    {
        return gameState == States.GameOver;
    }

    public bool IsPlay()
    {
        return gameState == States.Play;
    }
}
