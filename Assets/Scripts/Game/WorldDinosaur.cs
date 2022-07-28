using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WorldDinosaur : MonoBehaviour {
    [SerializeField] private Dinza[] dinosaurs;
    [SerializeField] private GameObject prefabToPoint;
    [SerializeField] private PanelDinza panelDinza;
    private bool isToched;
    private Dinza selectDinza;
    public void Init () {
        panelDinza.Init ();
        int length = dinosaurs.Length;
        for (int i = 0; i < length; i++) {
            dinosaurs[i].Init ();
        }
    }
    private void Update () {
        if(!MainController._mc.validSelect) return;
        if (Input.GetMouseButton (0) && !isToched) {
            isToched = true;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit)) {
                // Debug.Log (hit.transform.name);
                if (hit.transform.tag == "Floor") {
                    Vector3 posTo = hit.point;
                    posTo.y = 0;
                    if (selectDinza.posTo != null) {
                        selectDinza.posTo.transform.position = posTo;
                        selectDinza.Walk ();
                        //selectDinza = null;
                    }
                }
                if (hit.transform.tag == "Dinza") {
                    selectDinza = hit.transform.GetComponent<Dinza> ();
                    if (selectDinza.posTo == null) selectDinza.posTo = getToPoint ();
                    panelDinza.SelectDinza ();
                    SelectDinza();
                }
            }
        }
        if (Input.GetMouseButtonUp (0)) {
            isToched = false;
        }
    }
    private GameObject getToPoint () {
        return Instantiate (prefabToPoint, selectDinza.transform.position, Quaternion.identity);
    }
    public void ShowCameraSelectDinza () {
        if (selectDinza != null) {
            HideAllDinzaCamera ();
            MainController._mc.validSelect = false;
            selectDinza.CameraSee (true);
        }
    }
    public void HideCameraSelectDinza () {
        if (selectDinza != null) {
            MainController._mc.validSelect = true;
            selectDinza.CameraSee (false);
        }
    }
    private void HideAllDinzaCamera () {
        int length = dinosaurs.Length;
        for (int i = 0; i < length; i++) {
            dinosaurs[i].CameraSee (false);
        }
    }
    private void SelectDinza () {
        int length = dinosaurs.Length;
        for (int i = 0; i < length; i++) {
            dinosaurs[i].SelectObject (false);
        }
        selectDinza.SelectObject (true);
    }

}