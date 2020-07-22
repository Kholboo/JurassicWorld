using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PanelManager : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject hudPanel;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;

    public void ChangeState(Panels panelName, bool state = false, float delay = 0.0f)
    {
        StartCoroutine(_ChangeState(FindPanel(panelName), state, delay));
    }

    IEnumerator _ChangeState(GameObject panel, bool state, float time)
    {
        yield return new WaitForSeconds(time);
        panel.SetActive(state);
    }

    public GameObject FindPanel(Panels panel)
    {
        GameObject _panel = new GameObject();

        switch (panel)
        {
            case Panels.HOMEPANEL:
                _panel = homePanel;
                break;
            case Panels.HUDPANEL:
                _panel = hudPanel;
                break;
            case Panels.LEVELCOMPLETEPANEL:
                _panel = levelCompletePanel;
                break;
            case Panels.GAMEOVERPANEL:
                _panel = gameOverPanel;
                break;
        }

        return _panel;
    }
}

public enum Panels
{
    HOMEPANEL,
    HUDPANEL,
    LEVELCOMPLETEPANEL,
    GAMEOVERPANEL
}
