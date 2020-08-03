using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    float shakeDuration, shakeAmount, decreaseFactor;
    public bool shaketrue = false;
    Vector3 originalPos;

    void OnEnable () {
        originalPos = transform.localPosition;
    }

    void Update () {
        if (shaketrue) {
            if (shakeDuration > 0) {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;
            } else {
                transform.localPosition = originalPos;
                shaketrue = false;
            }
        }
    }

    public void ShakeCamera (float duration, float amount) {
        shakeDuration = duration;
        shakeAmount = amount;
        shaketrue = true;
    }
}