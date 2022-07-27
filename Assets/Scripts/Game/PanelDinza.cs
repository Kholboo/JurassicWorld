using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PanelDinza : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject btnCamHide, btnCamShow;
    [SerializeField] private WorldDinosaur worldDinosaur;
    public void Init () {
        mainCamera.enabled = true;
        btnCamHide.SetActive (false);
        btnCamShow.SetActive (false);
    }
    public void SelectDinza () {
        btnCamHide.SetActive (false);
        btnCamShow.SetActive (true);
    }
    public void ClickShow () {
        // mainCamera.enabled = false;
        btnCamHide.SetActive (true);
        btnCamShow.SetActive (false);
        worldDinosaur.ShowCameraSelectDinza();
    }
    public void ClickHide () {
        // mainCamera.enabled = true;
        btnCamHide.SetActive (false);
        btnCamShow.SetActive (false);
        worldDinosaur.HideCameraSelectDinza();
    }
}