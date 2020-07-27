using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
public class FollowFingerRotate : MonoBehaviour {
    public Transform center;
    [Range (0.1f, 1.0f)]
    public float speedRotate = 0.5f;
    [Range (1f, 50f)]
    public float radius = 5f;
    private int charDirect = 1;
    private RotateDirection rotateType;

    private string nameType = RotateDirection.RotateZ.ToString ();
    [DisableIf ("nameType", "RotateZ")]
    [HorizontalGroup ("Split", 0.5f)]
    [Button (ButtonSizes.Small)]
    private void RotateZ () {
        nameType = RotateDirection.RotateZ.ToString ();
        indicator = transform.position = Vector3.zero;
        center.transform.localRotation = Quaternion.identity;
        transform.position += Vector3.up * radius;

    }

    [DisableIf ("nameType", "RotateX")]
    [HorizontalGroup ("Split", 0.5f)]
    [Button (ButtonSizes.Small)]
    private void RotateX () {
        nameType = RotateDirection.RotateX.ToString ();
        indicator = transform.position = Vector3.zero;
        center.transform.localRotation = Quaternion.identity;
        transform.position += Vector3.right * radius;
    }

    [DisableIf ("nameType", "RotateY")]
    [HorizontalGroup ("Split", 0.5f)]
    [Button (ButtonSizes.Small)]
    private void RotateY () {
        nameType = RotateDirection.RotateY.ToString ();
        indicator = transform.position = Vector3.zero;
        center.transform.localRotation = Quaternion.identity;
        transform.position += Vector3.forward * radius;
    }

    //[Title ("Rotate CCW/CW")]
    private string ccw;
    // public string ccw 
    // {
    //     get {return _ccw;}
    //     set {_ccw = value;}
    // }
    [DisableIf ("ccw", "CCW")]
    [Button (ButtonSizes.Small)]
    private void CCW () {
        charDirect = 1;
        ccw = "CCW";
        ResetTransform();
    }

    [DisableIf ("ccw", "CW")]
    [Button (ButtonSizes.Small)]
    private void CW () { 
        ccw = "CW"; 
        charDirect = -1;
        ResetTransform();
    }

    public Vector2 moveStepUnit = new Vector2 (10, 10), moveRange;
    private Vector2 worldStartPos, worldDistance, deltaSpeed;
    private Vector3 posStart, posDistance, indicator;

    private void ResetTransform()
    {
        if(nameType == "RotateZ") RotateZ();
        if(nameType == "RotateX") RotateX();
        if(nameType == "RotateY") RotateY();
    }
    private void Start () {
        RotateZ ();
        CCW();
        transform.parent = center.transform;
        moveRange.x = moveStepUnit.x / 2;
        moveRange.y = moveStepUnit.y / 2;
        deltaSpeed.x = Screen.width;
        deltaSpeed.y = Screen.width;
    }

    private void GetDistanceMove () {
        posStart = Input.mousePosition;
        //Move X
        worldStartPos.x = ((moveStepUnit.x * Input.mousePosition.x) / deltaSpeed.x);
        worldDistance.x = worldStartPos.x - indicator.x;
        //Move Y
        worldStartPos.y = ((moveStepUnit.y * Input.mousePosition.y) / deltaSpeed.y);
        worldDistance.y = worldStartPos.y - indicator.z;
    }
    private void Update () {
        if (Input.GetMouseButtonDown (0)) {
            GetDistanceMove ();
        }
        if (Input.GetMouseButton (0)) {
            Movement ();
        }
    }
    private void Movement () {
        posDistance = Input.mousePosition - posStart;
        float goX = (moveStepUnit.x * posDistance.x) / deltaSpeed.x + worldStartPos.x - worldDistance.x;
        float goY = (moveStepUnit.y * posDistance.y) / deltaSpeed.y + worldStartPos.y - worldDistance.y;
        Vector3 posGo = new Vector3 (goX, indicator.y, goY);
        indicator = posGo;
        RotatingAround (posGo);
    }
    private void RotatingAround (Vector3 _value) {
        // print (_value);
        float angle = Mathf.Atan2 (center.position.y, center.position.x) * Mathf.Rad2Deg;
        switch (nameType) {
            case "RotateZ":
                center.transform.localRotation = Quaternion.AngleAxis ((angle + 90) * _value.x * speedRotate * charDirect, Vector3.forward);
                break;
            case "RotateX":
                center.transform.localRotation = Quaternion.AngleAxis ((angle - 90) * _value.x * speedRotate * charDirect, Vector3.up);
                break;
            case "RotateY":
                center.transform.localRotation = Quaternion.AngleAxis ((angle - 90) * _value.x * speedRotate * charDirect, Vector3.right);
                break;
            default:
                break;
        }

    }
}