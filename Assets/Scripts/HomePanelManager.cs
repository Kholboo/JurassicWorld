using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelManager : MonoBehaviour {
    GameManager gameManager;
    PanelManager panelManager;

    // Start is called before the first frame update
    void Start () {
        GameObject canvas = GameObject.Find ("Canvas");
        gameManager = canvas.GetComponent<GameManager> ();
        panelManager = canvas.GetComponent<PanelManager> ();
    }

    public void OnClickPlayBtn () {
        panelManager.CloseHomePanel ();
        gameManager.Play ();
        panelManager.OpenHUDPanel ();
    }
}