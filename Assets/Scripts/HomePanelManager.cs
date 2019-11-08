using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelManager : MonoBehaviour {
    public void OnClickPlayBtn () {
        GameManager.Instance.panelManager.ChangePanelState (PanelManager.Panels.HomePanel, false);
        GameManager.Instance.SetState (GameManager.States.Waiting);
        GameManager.Instance.panelManager.ChangePanelState (PanelManager.Panels.HUDPanel, true);
    }
}