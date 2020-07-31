using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void Play()
    {
        GetComponent<Animator>().Play("ButtonPress");
        GameManager.Instance.events.Test();

        GameManager.Instance.panelManager.ChangeState(Panels.HomePanel, false, 1f);
        GameManager.Instance.SetState(GameState.Play);
        GameManager.Instance.panelManager.ChangeState(Panels.HUDPanel, true, 1f);
    }
}