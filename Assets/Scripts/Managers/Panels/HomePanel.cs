using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour {
    public GameObject title, button;
    private void OnEnable () {
        StartCoroutine (WaitAndEnable (1f, title));
        StartCoroutine (WaitAndEnable (2f, button));
    }
    public void Play () {
        GetComponent<Animator> ().Play ("ButtonPress");
        GameManager.Instance.panelManager.ChangeState (Panels.HomePanel, false, 1f);
        GameManager.Instance.SetState (GameState.Play);
        GameManager.Instance.panelManager.ChangeState (Panels.HUDPanel, true, 1f);
    }
    IEnumerator WaitAndEnable (float _time, GameObject _gameObject) {
        yield return new WaitForSeconds (_time);
        _gameObject.SetActive (true);
    }
}