using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hero : MonoBehaviour {

    public MoveLineSlideController indicator;
    public enum FollowType {
        CUSTOM,
        HORIZONTAL,
        VERTICAL
    }
    public FollowType followType;
    public bool isInfinity = false;
    private Vector2 showMaxDistance;
    private void Start () {
        showMaxDistance.x = indicator.moveRange.x + 4;
        showMaxDistance.y = indicator.moveRange.y + 4;
    }
    //Run follow mouse....
    private void FixedUpdate () {
        Vector3 direction = indicator.transform.localPosition - transform.localPosition;
        Vector3 dir = direction;
        if (isInfinity) dir = Vector3.ClampMagnitude (indicator.transform.localPosition, showMaxDistance.x);
        transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation (dir), 0.2f);
        float targetX = transform.localPosition.x;
        float targetY = transform.localPosition.z;
        if (followType == FollowType.CUSTOM) {
            targetX = indicator.transform.localPosition.x;
            targetY = indicator.transform.localPosition.z;
        }
        if (followType == FollowType.HORIZONTAL) {
            targetX = indicator.transform.localPosition.x;
            if (isInfinity) targetX = transform.localPosition.x + indicator.transform.localPosition.x / 2f;
        }
        if (followType == FollowType.VERTICAL) {
            targetY = indicator.transform.localPosition.z;
            if (isInfinity) targetY = transform.localPosition.z + indicator.transform.localPosition.z / 2f;
        }
        Vector3 posTarget = new Vector3 (targetX, transform.localPosition.y, targetY);
        //Max range distance HORIZONTAL
        if (posTarget.x > indicator.moveRange.x) {
            posTarget.x = indicator.moveRange.x;
        }
        if (posTarget.x < -indicator.moveRange.x) {
            posTarget.x = -indicator.moveRange.x;
        }
        //Max range distance VERTICAL) {
        if (posTarget.z > indicator.moveRange.y) {
            posTarget.z = indicator.moveRange.y;
        }
        if (posTarget.z < -indicator.moveRange.y) {
            posTarget.z = -indicator.moveRange.y;
        }
        transform.localPosition = Vector3.Lerp (transform.localPosition, posTarget, 5f * Time.deltaTime);
        //transform.localPosition = Vector3.MoveTowards (transform.localPosition, posTarget, 100 * Time.deltaTime);
    }
    //Runing Bike.....
    // private void FixedUpdate () {
    //     Vector3 direction = indicator.transform.localPosition - transform.localPosition;
    //     Vector3 dir = Vector3.ClampMagnitude (indicator.transform.localPosition, maxDistance);
    //     transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation (dir), 0.2f);

    //     float targetX = transform.localPosition.x + indicator.transform.localPosition.x / 2f;
    //     Vector3 posTarget = new Vector3 (targetX, transform.localPosition.y, transform.localPosition.z);
    //     transform.localPosition = Vector3.Lerp (transform.localPosition, posTarget, 3f * Time.deltaTime);
    //     //transform.localPosition = Vector3.MoveTowards (transform.localPosition, posTarget, 100 * Time.deltaTime);
    // }
}