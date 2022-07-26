using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WorldDinosaur : MonoBehaviour {
    [SerializeField] private Dinza[] dinosaurs;
    [SerializeField] private GameObject prefabToPoint;    
    private bool isToched;
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
                    Vector3 posTo = hit.point;
                    posTo.y = 0;
                    if (selectDinza != null) {
                        selectDinza.posTo.transform.position = posTo;
                        selectDinza.Walk ();
                        selectDinza = null;
                    }
                }
                if (hit.transform.tag == "Dinza") {
                    selectDinza = hit.transform.GetComponent<Dinza> ();
                    if(selectDinza.posTo == null) selectDinza.posTo = getToPoint ();
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
}