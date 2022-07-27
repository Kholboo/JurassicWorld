using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class StaticMoveDinza : MonoBehaviour {
    [SerializeField] private Dinza dinza;
    [SerializeField] private bool revrese;
    [SerializeField] private GameObject moveTo, moveStart;
    [SerializeField] private int timeBack;
    private float speed = 1, speedRotate = 50;
    private int eventID;
    private void Start () {
        Init ();
    }
    public void Init () {
        dinza.Init ();
        dinza.Walk (true);
        dinza.transform.rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
        StartCoroutine (DoRotationAtTargetDirection (moveTo.transform, 1));
    }
    IEnumerator DoRotationAtTargetDirection (Transform opponentPlayer, int _animeID) {
        Quaternion targetRotation = Quaternion.identity;
        do {
            Vector3 targetDirection = (opponentPlayer.position - dinza.transform.position).normalized;
            targetRotation = Quaternion.LookRotation (targetDirection);
            dinza.transform.rotation = Quaternion.RotateTowards (dinza.transform.rotation, targetRotation, Time.deltaTime * speedRotate);
            yield return null;
        } while (Quaternion.Angle (dinza.transform.rotation, targetRotation) > 0.01f);
        eventID = _animeID;
        print (eventID + " >>> ");
    }
    private void Update () {
        if (eventID == 1) {
            dinza.transform.position = Vector3.MoveTowards (dinza.transform.position, moveTo.transform.position, Time.deltaTime * speed);
            float dist = Vector3.Distance (dinza.transform.position, moveTo.transform.position);
            if (dist < 0.1f) {
                eventID = 0;
                dinza.Roar ();
                print ("Drink..");
                if (revrese) {
                    // Invoke ("CallBack", timeBack);
                    StartCoroutine (CallAnimate (timeBack, 1, moveStart.transform));
                }
            }
        }
        if (eventID == 2) {
            dinza.transform.position = Vector3.MoveTowards (dinza.transform.position, moveStart.transform.position, Time.deltaTime * speed);                    
            float dist = Vector3.Distance (dinza.transform.position, moveStart.transform.position);
            if (dist < 0.1f) {
                eventID = 0;
                print ("Walk..");
                if (revrese) {
                    StartCoroutine (CallAnimate (timeBack, 2, moveTo.transform));
                }
            }
        }
    }
    IEnumerator CallAnimate (int _time, int _id, Transform _toTrans) {
        if (_id == 1) dinza.Walk ();
        if (_id == 2) dinza.Roar ();
        print (_id);
        yield return new WaitForSeconds (_time);
        StartCoroutine (DoRotationAtTargetDirection (_toTrans, _id));
    }
}