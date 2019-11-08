using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour {
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            GameManager.Instance.panelManager.ChangePanelState (PanelManager.Panels.HUDPanel, false);
            GameManager.Instance.SetState (GameManager.States.GameOver);
            GameManager.Instance.panelManager.ChangePanelState (PanelManager.Panels.GameOverPanel, true);
        }
    }
}