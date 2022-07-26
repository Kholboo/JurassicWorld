using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class Dinza : MonoBehaviour {
    [SerializeField] private Animator animator;
    private int eventID;
    private Transform posTo;
    private float speed = 3, speedRotate = 100;
    public void Init () {
        Idle ();
    }
    private void AnimateType (int _id) {
        animator.SetInteger ("state", _id);
    }
    public void Walk (Transform _posTo) {
        posTo = _posTo;
        eventID = 1;
        AnimateType (1);
        StartCoroutine (DoRotationAtTargetDirection (posTo));
    }
    public void Run () {
        eventID = 2;
        AnimateType (2);
    }
    public void Roar () {
        AnimateType (3);
    }
    public void Angry () {
        AnimateType (4);
    }
    private void Idle () {
        eventID = 0;
        AnimateType (0);
    }
    IEnumerator DoRotationAtTargetDirection (Transform opponentPlayer) {
        Quaternion targetRotation = Quaternion.identity;
        do {
            Vector3 targetDirection = (opponentPlayer.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation (targetDirection);
            transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * speedRotate);
            yield return null;
        } while (Quaternion.Angle (transform.rotation, targetRotation) > 0.01f);
    }
    private void Update () {
        if (eventID == 1) {
            transform.position = Vector3.MoveTowards (transform.position, posTo.position, Time.deltaTime * speed);
            // transform.Translate (posTo * speed * Time.deltaTime);            
            float dist = Vector3.Distance (transform.position, posTo.position);
            if (dist < 0.1f) {
                transform.position = posTo.position;
                Idle ();
            }
        }
    }
}