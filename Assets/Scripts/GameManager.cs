using System.Collections;
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
    PanelManager panelManager;
    LevelManager levelManager;

    void Start () {
        Application.targetFrameRate = 60;

        _instance = this;

        panelManager = GetComponent<PanelManager> ();
        levelManager = GetComponent<LevelManager> ();

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