using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum States { Start, Play, Waiting, GameOver, Completed, Replay }
    States gameState;
    PanelManager panelManager;
    static GameManager _instance;
    public static GameManager Instance {
        get { return _instance; }
    }

    void Start () {
        _instance = this;

        panelManager = GetComponent<PanelManager> ();
        Application.targetFrameRate = 60;

        if (PlayerPrefs.GetInt ("LevelUp", 0) == 1) {
            panelManager.ChangePanelState (PanelManager.Panel.HomePanel, false);
            PlayerPrefs.SetInt ("LevelUp", 0);
            SetState (States.Waiting);
            panelManager.ChangePanelState (PanelManager.Panel.HUDPanel, true);
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

    public void Replay () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }
}