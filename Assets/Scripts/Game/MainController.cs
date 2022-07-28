using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainController : MonoBehaviour {
    public static MainController _mc = null;
    [SerializeField] private WorldDinosaur worldDinosaur;
    public bool validSelect;
    private void Awake () {
        Application.targetFrameRate = 60;
        if (_mc == null) _mc = this;
        else Destroy (gameObject);
    }
    void Start () {
        validSelect = true;
        worldDinosaur.Init ();

    }
    void Update () {

    }
}