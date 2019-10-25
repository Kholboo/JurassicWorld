using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanelManager : MonoBehaviour {
    LevelManager levelManager;
    public Slider levelSlider;
    public Text currentLvlTxt;
    public Text nextLvlTxt;
    public Text scoreTxt;
    public List<GameObject> tips;
    bool hideTip;

    void Awake () {
        levelManager = GetComponentInParent<LevelManager> ();
    }

    void OnEnable () {
        currentLvlTxt.text = levelManager.GetLevel ().ToString ();
        nextLvlTxt.text = (levelManager.GetLevel () + 1).ToString ();

        currentLvlTxt.text = levelManager.GetLevel ().ToString ();
        nextLvlTxt.text = (levelManager.GetLevel () + 1).ToString ();

        if (GameManager.Instance.CheckState (GameManager.States.Waiting)) {
            tips[0].SetActive (true);
        }
    }

    void Update () {
        if (GameManager.Instance.CheckState (GameManager.States.Play)) {
            if (!hideTip) {
                hideTip = true;

                tips[0].SetActive (false);
            }
        }
    }
}