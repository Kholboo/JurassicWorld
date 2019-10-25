using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelManager : MonoBehaviour {
    PanelManager panelManager;

    // Start is called before the first frame update
    void Start () {
        GameObject canvas = GameObject.Find ("Canvas");
        panelManager = canvas.GetComponent<PanelManager> ();
    }

    public void OnClickPlayBtn () {
        panelManager.CloseHomePanel ();
        GameManager.Instance.Waiting ();
        panelManager.OpenHUDPanel ();
    }
}