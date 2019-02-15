using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject hudPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public void OpenHomePanel()
    {
        StartCoroutine(OpenPanel(homePanel));
    }

    public void CloseHomePanel()
    {
        StartCoroutine(ClosePanel(homePanel));
    }

    public void OpenHUDPanel()
    {
        StartCoroutine(OpenPanel(hudPanel));
    }

    public void CloseHUDPanel()
    {
        StartCoroutine(ClosePanel(hudPanel));
    }

    public void OpenGameOverPanel()
    {
        StartCoroutine(OpenPanel(gameOverPanel));
    }

    public void CloseGameOverPanel()
    {
        StartCoroutine(ClosePanel(gameOverPanel));
    }

    IEnumerator OpenPanel(GameObject openingPanel)
    {
        yield return new WaitForSeconds(0.15f);
        openingPanel.SetActive(true);
    }

    IEnumerator ClosePanel(GameObject closingPanel)
    {
        yield return new WaitForSeconds(0.15f);
        closingPanel.SetActive(true);
    }
}
