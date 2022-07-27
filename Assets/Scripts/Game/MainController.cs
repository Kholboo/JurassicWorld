using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainController : MonoBehaviour {
    [SerializeField] private WorldDinosaur worldDinosaur;
    float countTime, countAnimateTime;
    private void Awake() {
        Application.targetFrameRate = 60;    
    }
    void Start () {
        worldDinosaur.Init ();

    }
    void Update () {
       
    }
}