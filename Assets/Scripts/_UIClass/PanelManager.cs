using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {
    public enum Panels { HomePanel, HUDPanel, LevelCompletePanel, GameOverPanel }
    public List<GameObject> panels = new List<GameObject> ();

    public void ChangePanelState (Panels panelName, bool state = false, float delay = 0.0f) {
        StartCoroutine (_ChangePanelState (FindPanel (panelName), state, delay));
    }

    IEnumerator _ChangePanelState (GameObject panel, bool state, float time) {
        yield return new WaitForSeconds (time);
        panel.SetActive (state);
    }

    public GameObject FindPanel (Panels panel) {
        return panels.Find (p => p.name == panel.ToString ());
    }
}