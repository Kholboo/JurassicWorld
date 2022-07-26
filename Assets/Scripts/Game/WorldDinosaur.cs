using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WorldDinosaur : MonoBehaviour {
    [SerializeField] private Dinza[] dinosaurs;
    [SerializeField] private GameObject toPoint;
    private bool isToched;
    private Vector3 posTo;
    private Dinza selectDinza;
    public void Init () {
        int length = dinosaurs.Length;
        for (int i = 0; i < length; i++) {
            dinosaurs[i].Init ();
        }
    }
    private void Update () {
        if (Input.GetMouseButton (0) && !isToched) {
            isToched = true;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {
                // Debug.Log (hit.transform.name);
                if (hit.transform.tag == "Floor") {
                    posTo = hit.point;
                    posTo.y = 0;
                    if (selectDinza != null) {
                        toPoint.transform.position = posTo;
                        selectDinza.Walk (toPoint.transform);
                        selectDinza = null;
                    }
                }
                if (hit.transform.tag == "Dinza") {
                    selectDinza = hit.transform.GetComponent<Dinza> ();
                }
            }
        }
        if (Input.GetMouseButtonUp (0)) {
            isToched = false;
        }
    }
}