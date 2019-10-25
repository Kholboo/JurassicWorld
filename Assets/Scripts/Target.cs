using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    PanelManager panelManager;

    void Start () {
        GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
        panelManager = canvas.GetComponent<PanelManager> ();
    }

    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            panelManager.ChangePanelState (PanelManager.Panel.HUDPanel, false);
            GameManager.Instance.SetState (GameManager.States.Completed);
            panelManager.ChangePanelState (PanelManager.Panel.LevelCompletePanel, true);
        }
    }
}