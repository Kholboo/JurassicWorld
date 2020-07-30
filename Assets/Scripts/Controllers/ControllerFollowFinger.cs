using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
public class ControllerFollowFinger : MonoBehaviour {
    [Range (1.0f, 20.0f)]
    public float moveSpeed = 10.0f;
    [Range (0f, 10.0f)]
    public float rotateSpeed = 0.15f;
    [InfoBox ("Indicator movement range type")]
    public RangeType rangeType;
    public enum RangeType {
        RECTANGLE,
        ROUND
    }

    [InfoBox ("Follow object movement type")]
    public ControllerDirection movementType;
    private bool ShowStop = false;
    public bool FollowRotate = true;
    [ShowIf ("ShowStop", true)]
    public bool unStop = false;
    public Vector2 moveStepUnit = new Vector2 (10, 10), moveRange;
    private Vector2 worldStartPos, worldDistance, deltaSpeed;
    private Vector3 posStart, posCenter, posDistance;
    private bool isReset = false;
    private float resetTimer = 0;
    private GameObject parentChild;
    public GameObject indicatorChild;

    [CustomValueDrawer ("CreateHolder")]
    public bool isHolder;
    private bool CreateHolder (bool _holder, GUIContent label) {
        if (parentChild == null) {
            if (GameObject.Find ("HolderPlayer") != null) {
                parentChild = GameObject.Find ("HolderPlayer");
            } else {
                parentChild = new GameObject ("HolderPlayer");
            }
            parentChild.transform.parent = this.gameObject.transform.parent;
            this.transform.parent = parentChild.transform;
            transform.eulerAngles = Vector3.zero;
            parentChild.transform.eulerAngles = new Vector3 (0, 0, 0);
            CreateIndicator ();
        }
        return _holder;
    }

    [CustomValueDrawer ("AngleY")]
    [InfoBox ("Rotate Y to Holder (0, 90)",InfoMessageType.None)]
    public float roteteAngleY;
    private float AngleY (float value, GUIContent label) {
        if (parentChild != null) {
            parentChild.transform.eulerAngles = new Vector3 (0, value, 0);

        }
        return EditorGUILayout.Slider (value, 0f, 90f);;
    }

    private void CreateIndicator () {
        roteteAngleY = 0;
        parentChild.transform.eulerAngles = new Vector3 (0, 0, 0);
        if (GameObject.Find ("Indicator") != null) {
            indicatorChild = GameObject.Find ("Indicator");
        } else {
            indicatorChild = GameObject.CreatePrimitive (PrimitiveType.Sphere);
        }
        indicatorChild.name = "Indicator";
        indicatorChild.transform.parent = parentChild.transform;
        indicatorChild.AddComponent<MeshFilter> ();
        indicatorChild.AddComponent<MeshRenderer> ();
        indicatorChild.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
        indicatorChild.transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 2);
        Renderer rend = indicatorChild.GetComponent<Renderer> ();
        var tempMaterial = new Material (rend.sharedMaterial);
        tempMaterial.color = Color.red;
        indicatorChild.GetComponent<Renderer> ().sharedMaterial = tempMaterial;
        DestroyImmediate (indicatorChild.GetComponent<SphereCollider> ());
    }
    private void Start () {
        if (GameObject.Find ("HolderPlayer") != null) {
            parentChild = GameObject.Find ("HolderPlayer");
        }
        posCenter = Vector3.zero;
        moveRange.x = moveStepUnit.x / 2;
        moveRange.y = moveStepUnit.y / 2;
        deltaSpeed.x = Screen.width;
        deltaSpeed.y = Screen.width;
        //indicator = transform.localPosition;
    }

    private void GetDistanceMove () {
        posStart = Input.mousePosition;
        //Move X
        worldStartPos.x = ((moveStepUnit.x * Input.mousePosition.x) / deltaSpeed.x);
        worldDistance.x = worldStartPos.x - indicatorChild.transform.localPosition.x;
        //Move Y
        worldStartPos.y = ((moveStepUnit.y * Input.mousePosition.y) / deltaSpeed.y);
        worldDistance.y = worldStartPos.y - indicatorChild.transform.localPosition.z;
    }
    private void Update () {
        if (!FollowRotate) {
            if (transform.eulerAngles != Vector3.zero) transform.eulerAngles = Vector3.zero;
        }
        if ((rangeType == RangeType.RECTANGLE && movementType == ControllerDirection.Horizontal) || (rangeType == RangeType.RECTANGLE && movementType == ControllerDirection.Vertical)) {
            ShowStop = true;
        } else {
            ShowStop = false;
            unStop = false;
        }
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
        Vector3 posGo = new Vector3 (goX, indicatorChild.transform.localPosition.y, goY);
        if (rangeType == RangeType.RECTANGLE) {
            //Max range distance HORIZONTAL
            posGo.x = Mathf.Clamp (posGo.x, -moveRange.x, moveRange.x);
            //Max range distance VERTICAL
            posGo.z = Mathf.Clamp (posGo.z, -moveRange.y, moveRange.y);

            if (Mathf.Abs (posGo.x) >= moveRange.x || Mathf.Abs (posGo.z) >= moveRange.y) {
                isReset = true;
            }
        }
        if (rangeType == RangeType.ROUND) {
            float pos_dist = PointInsideSphere (posGo, posCenter, moveRange.x);
            if (pos_dist > moveRange.x) //Radius
            {
                Vector3 v = posGo - posCenter;
                v = Vector3.ClampMagnitude (v, moveRange.x);
                posGo = posCenter + v;
                isReset = true;
            }
        }
        indicatorChild.transform.localPosition = posGo;

        //INDICATOR.....
        MovementFollow ();
    }
    private void MovementFollow () {
        if (FollowRotate) {
            Vector3 dir = indicatorChild.transform.localPosition - transform.localPosition;
            if (unStop) dir = Vector3.ClampMagnitude (indicatorChild.transform.localPosition, moveStepUnit.x);
            if (dir.magnitude > 0.1f) {
                transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation (dir), rotateSpeed);
            }
        }
        float targetX = indicatorChild.transform.localPosition.x;
        float targetY = indicatorChild.transform.localPosition.z;

        Vector3 posTarget = new Vector3 (targetX, transform.localPosition.y, targetY);
        if (movementType == ControllerDirection.Horizontal) {
            posTarget = new Vector3 (targetX, transform.localPosition.y, transform.localPosition.z);
        }
        if (movementType == ControllerDirection.Vertical) {
            posTarget = new Vector3 (transform.localPosition.x, transform.localPosition.y, targetY);
        }

        if (unStop) {
            if (movementType == ControllerDirection.Horizontal) {
                targetX = transform.localPosition.x + indicatorChild.transform.localPosition.x / 2f;
                targetY = transform.localPosition.z;
            }
            if (movementType == ControllerDirection.Vertical) {
                targetX = transform.localPosition.x;
                targetY = transform.localPosition.z + indicatorChild.transform.localPosition.z / 2f;
            }
            posTarget = new Vector3 (targetX, transform.localPosition.y, targetY);
        }

        transform.localPosition = Vector3.Lerp (transform.localPosition, posTarget, moveSpeed * Time.deltaTime);
    }
    private float PointInsideSphere (Vector3 point, Vector3 center, float radius) {
        return Vector3.Distance (point, center);
    }
}