using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFinger : MonoBehaviour {
    [Range (0,90)]
    public float roteteAngleY = 0;
    public enum MovementType {
        CUSTOM,
        HORIZONTAL,
        VERTICAL
    }
    public enum BorderType {
        RECTANGLE,
        ROUND
    }
    public MovementType movementType;
    public BorderType borderType;
    //[SerializeField]
    public Vector2 moveStepUnit, moveRange;
    Vector2 worldStartPos, worldDistance, deltaSpeed;
    Vector3 posStart, posCenter, posDistance;
    GameObject ground;
    bool isReset = false;
    float resetTimer = 0;
    private void Start () {
        //transform.GetComponent<MeshRenderer>().enabled = false;
        posCenter = Vector3.zero;
        moveStepUnit.x = 10;
        moveStepUnit.y = 10;
        moveRange.x = moveStepUnit.x / 2;
        moveRange.y = moveStepUnit.y / 2;
        deltaSpeed.x = Screen.width;
        deltaSpeed.y = Screen.width;
        //Ground and icons show/hide
        ground = GameObject.FindGameObjectWithTag ("Ground");
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
            posDistance = Input.mousePosition - posStart;
            float goX = (moveStepUnit.x * posDistance.x) / deltaSpeed.x + worldStartPos.x - worldDistance.x;
            float goY = (moveStepUnit.y * posDistance.y) / deltaSpeed.y + worldStartPos.y - worldDistance.y;
            Vector3 pos_go = new Vector3 (goX, transform.localPosition.y, goY);
            if (movementType == MovementType.HORIZONTAL) {
                pos_go = new Vector3 (goX, transform.localPosition.y, transform.localPosition.z);
            }
            if (movementType == MovementType.VERTICAL) {
                pos_go = new Vector3 (transform.localPosition.x, transform.localPosition.y, goY);
            }
            if (borderType == BorderType.RECTANGLE) {
                //Max range distance HORIZONTAL
                if (pos_go.x > moveRange.x) {
                    pos_go.x = moveRange.x;
                    isReset = true;
                }
                if (pos_go.x < -moveRange.x) {
                    pos_go.x = -moveRange.x;
                    isReset = true;
                }
                //Max range distance VERTICAL
                if (pos_go.z > moveRange.y) {
                    pos_go.z = moveRange.y;
                    isReset = true;
                }
                if (pos_go.z < -moveRange.y) {
                    pos_go.z = -moveRange.y;
                    isReset = true;
                }
            }
            if (borderType == BorderType.ROUND) {
                float pos_dist = PointInsideSphere (pos_go, posCenter, moveRange.x);
                if (pos_dist > moveRange.x) //If the distance is less than the radius, it is already within the circle.
                {
                    Vector3 v = pos_go - posCenter;
                    v = Vector3.ClampMagnitude (v, moveRange.x);
                    pos_go = posCenter + v;
                    isReset = true;
                }
            }
            transform.localPosition = pos_go;

            //     Vector3 direction = indicator.transform.localPosition - transform.localPosition;
            //     Vector3 dir = Vector3.ClampMagnitude (indicator.transform.localPosition, maxDistance);
            //     transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation (dir), 0.2f);
        }
        if (isReset) {
            resetTimer += Time.deltaTime;
            if(resetTimer >= 0.2f)
            {
                resetTimer = 0;
                isReset = false;
                GetDistanceMove();
            }
        }
    }
    float PointInsideSphere (Vector3 point, Vector3 center, float radius) {
        //return Vector3.Distance(point, center) < radius;
        return Vector3.Distance (point, center);
    }

    //Help icons....
    private void ShowIcons (int _id, int _child = 0) {
        GameObject holderIcon = ground.transform.GetChild (_child).gameObject;
        foreach (Transform item in holderIcon.transform) {
            item.gameObject.SetActive (false);
        }
        GameObject selectIcon = holderIcon.transform.GetChild (_id).gameObject;
        selectIcon.SetActive (true);
    }
    private void LateUpdate () {
        if (movementType == MovementType.CUSTOM) {
            ShowIcons (0);
        }
        if (movementType == MovementType.HORIZONTAL) {
            borderType = BorderType.RECTANGLE;
            ShowIcons (1);
        }
        if (movementType == MovementType.VERTICAL) {
            borderType = BorderType.RECTANGLE;
            ShowIcons (2);
        }
        if (borderType == BorderType.RECTANGLE) {
            ShowIcons (0, 1);
        }
        if (borderType == BorderType.ROUND) {
            ShowIcons (1, 1);
        }
        transform.parent.transform.eulerAngles = new Vector3(0,roteteAngleY,0);
    }
}