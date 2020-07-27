using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
public class FollowFinger : MonoBehaviour {
    [Range (1.0f, 20.0f)]
    public float moveSpeed = 10.0f;
    [Range (0f, 10.0f)]
    public float rotateSpeed = 0.15f;
    public enum BorderType {
        RECTANGLE,
        ROUND
    }
    public ControllerDirection movementType;
    public BorderType borderType;
    public bool FollowRotate = true;
    public bool unStop = false;
    public Vector2 moveStepUnit = new Vector2 (10, 10), moveRange;
    private Vector2 worldStartPos, worldDistance, deltaSpeed;
    private Vector3 posStart, posCenter, posDistance;
    private Vector3 indicator;
    private bool isReset = false;
    private float resetTimer = 0;
    private GameObject parentChild;
    [ReadOnly]
    public bool isHolder;

    [HideIf ("isHolder", true)]
    [Button (ButtonSizes.Medium)]
    public void CreateHolder () {
        isHolder = !isHolder;
        CreateParentChild ();
    }

    [ShowIf ("isHolder", true)]
    [Button (ButtonSizes.Medium)]
    public void DeleteHolder () {
        isHolder = !isHolder;
        RemoveParentChild ();
    }

    [ShowIf ("isHolder", true)]
    [CustomValueDrawer ("AngleY")]
    public float roteteAngleY;
    private float AngleY (float value, GUIContent label) {
        if (parentChild != null) {
            parentChild.transform.eulerAngles = new Vector3 (0, value, 0);

        }
        return EditorGUILayout.Slider (value, 0f, 90f);;
    }

    public void CreateParentChild () {
        if (GameObject.Find ("HolderPlayer") != null) {
            parentChild = GameObject.Find ("HolderPlayer");
        } else {
            parentChild = new GameObject ("HolderPlayer");
        }
        parentChild.transform.parent = this.gameObject.transform.parent;
        this.transform.parent = parentChild.transform;
        transform.eulerAngles = Vector3.zero;
    }
    public void RemoveParentChild () {
        roteteAngleY = 0;

        if (this.gameObject.transform.parent != null) {
            this.transform.parent = this.gameObject.transform.parent.transform.parent;
            GameObject.DestroyImmediate (parentChild);
        }
        if (GameObject.Find ("HolderPlayer") != null) {
            DestroyImmediate (GameObject.Find ("HolderPlayer").gameObject);
        }
    }

    private void Awake () {

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
        indicator = transform.localPosition;
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
        Vector3 posGo = new Vector3 (goX, indicator.y, goY);
        if (movementType == ControllerDirection.Horizontal) {
            posGo = new Vector3 (goX, indicator.y, indicator.z);
        }
        if (movementType == ControllerDirection.Vertical) {
            posGo = new Vector3 (indicator.x, indicator.y, goY);
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
        indicator = posGo;

        //INDICATOR.....
        MovementIndicator (goY);
    }
    private void MovementIndicator (float _y) {
        if (FollowRotate) {
            Vector3 smoothLook = new Vector3(indicator.x,indicator.y, _y);
            print(smoothLook.z);
            // RaycastHit hit;
            // Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            // if (Physics.Raycast (ray, out hit, 100)) {
            //     //Transform objectHit = hit.transform;
            //     smoothLook = new Vector3 (indicator.x, indicator.y, hit.point.z);
            //     //print(hit.point+" >>>>> "+smoothLook);
            //     // Do something with the object that was hit by the raycast.
            // }

            Vector3 dir = smoothLook - transform.localPosition;
            if (unStop) dir = Vector3.ClampMagnitude (smoothLook, moveStepUnit.x);
            //if (dir.magnitude > 0.1f) 
            {
                transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation (dir), rotateSpeed);
            }
        }
        float targetX = indicator.x;
        float targetY = indicator.z;
        if (unStop) {
            //if (movementType == ControllerDirection.Horizontal) 
            {
                //print(smoothLook);
                targetX = transform.localPosition.x + indicator.x / 2f;
                targetY = transform.localPosition.z + indicator.z / 2f;
            }
        }
        Vector3 posTarget = new Vector3 (targetX, transform.localPosition.y, targetY);
        transform.localPosition = Vector3.Lerp (transform.localPosition, posTarget, moveSpeed * Time.deltaTime);
    }
    private float PointInsideSphere (Vector3 point, Vector3 center, float radius) {
        return Vector3.Distance (point, center);
    }
}