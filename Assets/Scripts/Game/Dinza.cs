using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class Dinza : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private bool autoRotate = true;
    [SerializeField] private int animateID;
    [SerializeField] private CinemachineVirtualCamera fCamera;
    private int eventID;
    public GameObject posTo;
    public float speed = 3, speedRotate = 100;
    public void Init () {
        if(fCamera != null) fCamera.enabled = false;
        Idle ();
        if (autoRotate)
            transform.rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
        if (animateID != 0 && posTo == null)
            AnimateType (animateID);
    }
    private void AnimateType (int _id) {
        print ("animate: " + _id);
        animator.SetInteger ("state", _id);
    }
    public void Walk (bool _start = false) {
        // eventID = 1;        
        AnimateType (1);
        if (!_start) StartCoroutine (DoRotationAtTargetDirection (posTo.transform));
    }
    public void Run () {
        eventID = 2;
        AnimateType (2);
    }
    public void Roar () {
        eventID = 0;
        AnimateType (3);
    }
    public void Angry () {
        eventID = 0;
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
                if (animateID == 0) Idle ();
                else AnimateType (animateID);
            }
        }
    }
    public void CameraSee(bool _show)
    {
        if(fCamera != null) fCamera.enabled = _show;
    }
}