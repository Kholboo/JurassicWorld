using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {
    public GameObject homePanel;
    public GameObject hudPanel;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;

    public void OpenHomePanel () {
        StartCoroutine (OpenPanel (homePanel));
    }

    public void CloseHomePanel () {
        StartCoroutine (ClosePanel (homePanel));
    }

    public void OpenHUDPanel () {
        StartCoroutine (OpenPanel (hudPanel));
    }

    public void CloseHUDPanel () {
        StartCoroutine (ClosePanel (hudPanel));
    }

    public void OpenGameOverPanel () {
        StartCoroutine (OpenPanel (gameOverPanel));
    }

    public void CloseGameOverPanel () {
        StartCoroutine (ClosePanel (gameOverPanel));
    }

    public void OpenLevelCompletePanel () {
        StartCoroutine (OpenPanel (levelCompletePanel));
    }

    public void CloseLevelCompletePanel () {
        StartCoroutine (ClosePanel (levelCompletePanel));
    }

    IEnumerator OpenPanel (GameObject panel) {
        yield return new WaitForSeconds (0.15f);
        panel.SetActive (true);
    }

    IEnumerator ClosePanel (GameObject panel) {
        yield return new WaitForSeconds (0.15f);
        panel.SetActive (false);
    }
}