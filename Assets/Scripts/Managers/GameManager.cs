using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    
}

public enum GameState
{
    Start,
    Play,
    GameOver,
    Completed,
    Replay,
    Pause
}
