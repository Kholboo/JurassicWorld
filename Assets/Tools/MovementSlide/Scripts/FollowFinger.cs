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
    
    bool isReset = false;
    float resetTimer = 0;
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
            posDistance = Input.mousePosition - posStart;
            float goX = (moveStepUnit.x * posDistance.x) / deltaSpeed.x + worldStartPos.x - worldDistance.x;
            float goY = (moveStepUnit.y * posDistance.y) / deltaSpeed.y + worldStartPos.y - worldDistance.y;
            Vector3 posGo = new Vector3 (goX, transform.localPosition.y, goY);
            if (movementType == MovementType.HORIZONTAL) {
                posGo = new Vector3 (goX, transform.localPosition.y, transform.localPosition.z);
            }
            if (movementType == MovementType.VERTICAL) {
                posGo = new Vector3 (transform.localPosition.x, transform.localPosition.y, goY);
            }
            if (borderType == BorderType.RECTANGLE) {
                //Max range distance HORIZONTAL
                if (posGo.x > moveRange.x) {
                    posGo.x = moveRange.x;
                    isReset = true;
                }
                if (posGo.x < -moveRange.x) {
                    posGo.x = -moveRange.x;
                    isReset = true;
                }
                //Max range distance VERTICAL
                if (posGo.z > moveRange.y) {
                    posGo.z = moveRange.y;
                    isReset = true;
                }
                if (posGo.z < -moveRange.y) {
                    posGo.z = -moveRange.y;
                    isReset = true;
                }
            }
            if (borderType == BorderType.ROUND) {
                float pos_dist = PointInsideSphere (posGo, posCenter, moveRange.x);
                if (pos_dist > moveRange.x) //If the distance is less than the radius, it is already within the circle.
                {
                    Vector3 v = posGo - posCenter;
                    v = Vector3.ClampMagnitude (v, moveRange.x);
                    posGo = posCenter + v;
                    isReset = true;
                }
            }
            transform.localPosition = posGo;

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

   
}