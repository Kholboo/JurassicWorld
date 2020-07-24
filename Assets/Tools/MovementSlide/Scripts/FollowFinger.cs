using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowFinger : MonoBehaviour {
    private float _rotateAngle;
    public float roteteAngleY {
        get { return _rotateAngle; }
        set { _rotateAngle = value; }
    }
    public enum BorderType {
        RECTANGLE,
        ROUND
    }
    public ControllerDirection movementType;
    public BorderType borderType;
    public Vector2 moveStepUnit, moveRange;
    private Vector2 worldStartPos, worldDistance, deltaSpeed;
    private Vector3 posStart, posCenter, posDistance, posDirection;

    private bool isReset = false;
    private float resetTimer = 0, directionTimer;
    private GameObject parentChild;
    public void CreateParentChild () {
        parentChild = new GameObject ("HolderPlayer");
        parentChild.transform.parent = this.gameObject.transform.parent;
        this.transform.parent = parentChild.transform;
        transform.eulerAngles = Vector3.zero;
        roteteAngleY = 0;
    }
    public void RemoveParentChild () {
        roteteAngleY = 0;
        if (this.gameObject.transform.parent != null) {
            this.transform.parent = this.gameObject.transform.parent.transform.parent;
            GameObject.DestroyImmediate (parentChild);
        }
    }
    public void RotateHolder () {
        if (parentChild != null) {
            parentChild.transform.eulerAngles = new Vector3 (0, roteteAngleY, 0);
        }
    }
    private void Start () {
        //transform.GetComponent<MeshRenderer>().enabled = false;
        posCenter = Vector3.zero;
        moveRange.x = moveStepUnit.x / 2;
        moveRange.y = moveStepUnit.y / 2;
        deltaSpeed.x = Screen.width;
        deltaSpeed.y = Screen.width;
    }

    private void GetDistanceMove () {
        posStart = Input.mousePosition;
        //Move X
        worldStartPos.x = ((moveStepUnit.x * Input.mousePosition.x) / deltaSpeed.x);
        worldDistance.x = worldStartPos.x - transform.localPosition.x;
        //Move Y
        worldStartPos.y = ((moveStepUnit.y * Input.mousePosition.y) / deltaSpeed.y);
        worldDistance.y = worldStartPos.y - transform.localPosition.z;
    }
    private void Update () {
        if (Input.GetMouseButtonDown (0)) {
            GetDistanceMove ();
        }
        if (Input.GetMouseButton (0)) {
            Movement ();
        }
        if (isReset) {
            resetTimer += Time.deltaTime;
            if (resetTimer >= 0.2f) {
                resetTimer = 0;
                isReset = false;
                GetDistanceMove ();
            }
        }
        if (parentChild != null) {
            parentChild.transform.eulerAngles = new Vector3 (0, roteteAngleY, 0);
        }
    }
    private void Movement () {
        posDistance = Input.mousePosition - posStart;
        float goX = (moveStepUnit.x * posDistance.x) / deltaSpeed.x + worldStartPos.x - worldDistance.x;
        float goY = (moveStepUnit.y * posDistance.y) / deltaSpeed.y + worldStartPos.y - worldDistance.y;
        Vector3 posGo = new Vector3 (goX, transform.localPosition.y, goY);
        if (movementType == ControllerDirection.Horizontal) {
            posGo = new Vector3 (goX, transform.localPosition.y, transform.localPosition.z);
        }
        if (movementType == ControllerDirection.Vertical) {
            posGo = new Vector3 (transform.localPosition.x, transform.localPosition.y, goY);
        }
        if (borderType == BorderType.RECTANGLE) {
            //Max range distance HORIZONTAL
            posGo.x = Mathf.Clamp (posGo.x, -moveRange.x, moveRange.x);
            //Max range distance VERTICAL
            posGo.z = Mathf.Clamp (posGo.z, -moveRange.y, moveRange.y);

            if (Mathf.Abs (posGo.x) >= moveRange.x || Mathf.Abs (posGo.z) >= moveRange.y) {
                isReset = true;
            }
        }
        if (borderType == BorderType.ROUND) {
            float pos_dist = PointInsideSphere (posGo, posCenter, moveRange.x);
            if (pos_dist > moveRange.x) //Radius
            {
                Vector3 v = posGo - posCenter;
                v = Vector3.ClampMagnitude (v, moveRange.x);
                posGo = posCenter + v;
                isReset = true;
            }
        }
        transform.localPosition = posGo;
        directionTimer += Time.deltaTime;
        if (directionTimer >= 0.01f) {
            directionTimer = 0;
            posDirection = transform.localPosition;
        }
        //     Vector3 direction = indicator.transform.localPosition - transform.localPosition;
        //     Vector3 dir = Vector3.ClampMagnitude (indicator.transform.localPosition, maxDistance);
        //     transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation (dir), 0.2f);
    }
    private void FixedUpdate () {
        directionTimer = 0;
        Vector3 dir = posDirection - transform.localPosition;
        //if (isInfinity) dir = Vector3.ClampMagnitude (indicator.transform.localPosition, showMaxDistance.x);
        transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation (dir), 0.2f);
    }
    float PointInsideSphere (Vector3 point, Vector3 center, float radius) {
        //return Vector3.Distance(point, center) < radius;
        return Vector3.Distance (point, center);
    }
}