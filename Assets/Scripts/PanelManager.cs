using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {
    public enum Panels { HomePanel, HUDPanel, LevelCompletePanel, GameOverPanel }
    public List<GameObject> panels = new List<GameObject> ();

    public void ChangePanelState (Panels panelName, bool state = false, float delay = 0.0f) {
        GameObject panel = panels.Find (p => p.name == panelName.ToString ());

        StartCoroutine (_ChangePanelState (panel, state, delay));
    }

    IEnumerator _ChangePanelState (GameObject panel, bool state, float time) {
        yield return new WaitForSeconds (time);
        panel.SetActive (state);
    }
}