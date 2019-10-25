using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    PanelManager panelManager;

    void Start () {
        GameObject canvas = GameObject.Find ("Canvas");
        panelManager = canvas.GetComponent<PanelManager> ();
    }

    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            panelManager.CloseHUDPanel ();
            GameManager.Instance.Completed ();
            panelManager.OpenLevelCompletedPanel ();
        }
    }
}