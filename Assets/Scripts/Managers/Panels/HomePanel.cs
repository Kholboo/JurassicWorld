using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public GameObject playButton;

    public void Play()
    {
        playButton.GetComponent<Animator>().Play("ButtonPress");
        GameManager.Instance.panelManager.ChangeState(Panels.HomePanel, false, 0.2f);
        GameManager.Instance.SetState(GameState.Play);
        GameManager.Instance.panelManager.ChangeState(Panels.HUDPanel, true, 0.2f);
    }
}