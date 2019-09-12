using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {
    public GameObject homePanel;
    public GameObject hudPanel;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;

    public void OpenHomePanel () {
        homePanel.SetActive (true);
    }

    public void CloseHomePanel () {
        homePanel.SetActive (false);
    }

    public void OpenHUDPanel () {
        hudPanel.SetActive (true);
    }

    public void CloseHUDPanel () {
        hudPanel.SetActive (false);
    }

    public void OpenGameOvePanel () {
        gameOverPanel.SetActive (true);
    }

    public void CloseGameOverPanel () {
        gameOverPanel.SetActive (false);
    }

    public void OpenLevelCompletedPanel () {
        levelCompletePanel.SetActive (true);
    }

    public void CloseLevelCompletedPanel () {
        levelCompletePanel.SetActive (false);
    }
}