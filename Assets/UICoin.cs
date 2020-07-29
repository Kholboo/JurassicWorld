using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class UICoin : MonoBehaviour {
    bool isSpread = true;
    Vector3 euler, spreadPositon;
    float randomSpeed;

    void Start () {
        euler = transform.eulerAngles;
        euler.z = Random.Range (0f, 360f);
        randomSpeed = Random.Range (0.8f, 1f);
        transform.eulerAngles = euler;
        transform.localScale = new Vector3 (SpreadCollectable.instance.startSize, SpreadCollectable.instance.startSize, SpreadCollectable.instance.startSize);
        StartCoroutine ("WaitForSend");
    }

    void LateUpdate () {
        if (isSpread) {
            Spread ();
        }
        if (!isSpread) {
            transform.localPosition = Vector3.Lerp (transform.localPosition, Vector3.zero, SpreadCollectable.instance.speed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.identity, SpreadCollectable.instance.speed * Time.deltaTime);
            if (transform.localScale.x >= 0) {
                transform.localScale -= new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);
            }
        }
    }
    public void Spread () {
        transform.localPosition = Vector3.Lerp (transform.localPosition, spreadPositon, SpreadCollectable.instance.spreadSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.RotateTowards (transform.rotation, new Quaternion (0, 0, SpreadCollectable.instance.randomRotation.z, 1), 100.0f * Time.deltaTime);
    }
    IEnumerator WaitForSend () {
        transform.SetParent (SpreadCollectable.instance.transform);
        spreadPositon = new Vector3 (transform.localPosition.x + Random.Range (-SpreadCollectable.instance.spreadRangeX, SpreadCollectable.instance.spreadRangeX), transform.localPosition.y + Random.Range (-SpreadCollectable.instance.spreadRangeY, SpreadCollectable.instance.spreadRangeY), transform.localPosition.z);
        yield return new WaitForSeconds (randomSpeed);
          GameManager.Instance.tapticManager.Impact(HapticTypes.HeavyImpact);
        transform.SetParent (SpreadCollectable.instance.targetObject.transform);
        isSpread = false;
    }
}