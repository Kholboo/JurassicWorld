using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainController : MonoBehaviour {
    [SerializeField] private WorldDinosaur worldDinosaur;
    float countTime, countAnimateTime;
    void Start () {
        worldDinosaur.Init ();

    }

    // Update is called once per frame
    void Update () {
        // countTime += Time.deltaTime;
        // if(countTime > 2)
        // {
        //     dinza.Walk();
        //     countAnimateTime += Time.deltaTime;
        //     if(countAnimateTime > 5)
        //     {
        //         dinza.Run();
        //         countTime = 0;
        //         countAnimateTime = 0;
        //     }
        // }
    }
}