using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour {
    GameManager gameManager;
    PanelManager panelManager;

    void Start () {
        GameObject canvas = GameObject.Find ("Canvas");
        gameManager = canvas.GetComponent<GameManager> ();
        panelManager = canvas.GetComponent<PanelManager> ();
    }

    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            panelManager.CloseHUDPanel ();
            gameManager.GameOver ();
            panelManager.OpenGameOvePanel ();
        }
    }
}