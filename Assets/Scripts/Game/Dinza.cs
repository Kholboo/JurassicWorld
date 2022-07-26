using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class Dinza : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private bool autoRotate = true;
    [SerializeField] private int animateID;
    private int eventID;
    public GameObject posTo;
    public float speed = 3, speedRotate = 100;
    public void Init () {
        Idle ();
        if (autoRotate)
            transform.rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
        if (animateID != 0)
            AnimateType (animateID);
    }
    private void AnimateType (int _id) {
        print("animate: "+_id);
        animator.SetInteger ("state", _id);
    }
    public void Walk () {
        // eventID = 1;
        AnimateType (1);
        StartCoroutine (DoRotationAtTargetDirection (posTo.transform));
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
        eventID = 1;
    }
    private void Update () {
        if (eventID == 1) {
            transform.position = Vector3.MoveTowards (transform.position, posTo.transform.position, Time.deltaTime * speed);
            // transform.Translate (posTo * speed * Time.deltaTime);            
            float dist = Vector3.Distance (transform.position, posTo.transform.position);
            if (dist < 0.1f) {
                transform.position = posTo.transform.position;
                Idle ();
            }
        }
    }
}