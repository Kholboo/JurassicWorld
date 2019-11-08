using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            GameManager.Instance.panelManager.ChangePanelState (PanelManager.Panels.HUDPanel, false);
            GameManager.Instance.SetState (GameManager.States.Completed);
            GameManager.Instance.panelManager.ChangePanelState (PanelManager.Panels.LevelCompletePanel, true);
        }
    }
}