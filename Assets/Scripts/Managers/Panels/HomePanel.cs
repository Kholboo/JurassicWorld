using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.panelManager.ChangeState(Panels.HomePanel, false);
        GameManager.Instance.SetState(GameState.Play);
        GameManager.Instance.panelManager.ChangeState(Panels.HUDPanel, true);
    }
}