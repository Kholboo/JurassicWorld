using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanelManager : MonoBehaviour {
    public Slider levelSlider;
    public Text currentLvlTxt;
    public Text nextLvlTxt;
    public Text scoreTxt;
    public List<GameObject> tips;
    bool hideTip;

    void OnEnable () {
        currentLvlTxt.text = GameManager.Instance.levelManager.GetLevel ().ToString ();
        nextLvlTxt.text = (GameManager.Instance.levelManager.GetLevel () + 1).ToString ();

        if (GameManager.Instance.CheckState (GameManager.States.Waiting)) {
            tips[0].SetActive (true);
        }
    }

    void Update () {
        if (GameManager.Instance.CheckState (GameManager.States.Waiting)) {
            if (Input.GetMouseButtonDown (0)) {
                GameManager.Instance.SetState (GameManager.States.Play);
            }
        }

        if (GameManager.Instance.CheckState (GameManager.States.Play)) {
            if (!hideTip) {
                hideTip = true;

                tips[0].SetActive (false);
            }
        }
    }
}