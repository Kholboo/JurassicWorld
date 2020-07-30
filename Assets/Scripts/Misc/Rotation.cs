using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    public bool x, y, z, reverse;
    [Range (30, 500)]
    public int speed;

    // Update is called once per frame
    void LateUpdate () {
        if (x) {
            if (reverse) {
                transform.RotateAround (transform.position, -transform.right, Time.deltaTime * speed);
            } else {
                transform.RotateAround (transform.position, transform.right, Time.deltaTime * speed);
            }
        }
        if (y) {
            if (reverse) {
                transform.RotateAround (transform.position, -transform.up, Time.deltaTime * speed);
            } else {
                transform.RotateAround (transform.position, transform.up, Time.deltaTime * speed);
            }
        }
        if (z) {
            if (reverse) {
                transform.RotateAround (transform.position, -transform.forward, Time.deltaTime * speed);
            } else {
                transform.RotateAround (transform.position, transform.forward, Time.deltaTime * speed);
            }
        }
    }
}