using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelManager : MonoBehaviour {
    PanelManager panelManager;

    void Awake () {
        panelManager = GetComponentInParent<PanelManager> ();
    }

    public void OnClickPlayBtn () {
        panelManager.ChangePanelState (PanelManager.Panel.HomePanel, false);
        GameManager.Instance.SetState (GameManager.States.Waiting);
        panelManager.ChangePanelState (PanelManager.Panel.HUDPanel, true);
    }
}