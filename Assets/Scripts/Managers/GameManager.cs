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
    [EnumToggleButtons]
    public GameState gameState;
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

    void Awake()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        
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

        if (levelManager.IsLevelUp())
        {
            panelManager.ChangeState(Panels.HomePanel, false);

            levelManager.LevelUp(false);
            SetState(GameState.Start);

            panelManager.ChangeState(Panels.HUDPanel, true);
        }
    }

    public bool CheckState(GameState state)
    {
        return gameState == state;
    }

    public void SetState(GameState state)
    {
        if (state == GameState.Replay)
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

public enum GameState
{
    Start,
    Play,
    GameOver,
    Completed,
    Replay,
    Pause
}
