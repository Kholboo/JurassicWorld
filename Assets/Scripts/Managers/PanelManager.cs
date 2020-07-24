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

    public void ChangeState(Panels panel, bool state = true, float delay = 0.0f)
    {
        StartCoroutine(_ChangeState(FindPanel(panel), state, delay));
    }

    IEnumerator _ChangeState(GameObject panel, bool state, float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(state);
    }

    GameObject FindPanel(Panels panel)
    {
        GameObject _panel = new GameObject();

        switch (panel)
        {
            case Panels.HomePanel:
                _panel = homePanel;
                break;
            case Panels.HUDPanel:
                _panel = hudPanel;
                break;
            case Panels.LevelCompletePanel:
                _panel = levelCompletePanel;
                break;
            case Panels.GameOverPanel:
                _panel = gameOverPanel;
                break;
        }

        return _panel;
    }
}

public enum Panels
{
    HomePanel,
    HUDPanel,
    LevelCompletePanel,
    GameOverPanel
}

public enum PanelState
{
    Open,
    Close
}
