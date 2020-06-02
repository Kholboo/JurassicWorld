using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum ControllerType {
        JOYSTICK,
    }
    public ControllerType controllerType;
    public DynamicJoystick joystick;
    float moveSpeed = 10.0f;
    float rotateSpeed = 10.0f;

    void FixedUpdate () {
        if (GameManager.Instance.CheckState (GameManager.States.Play)) {
            switch (controllerType) {
                case ControllerType.JOYSTICK:
                    Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
                    MoveJoystick (direction);
                    break;
            }
        }
    }

    void MoveJoystick (Vector3 direction) {
        Vector3 target = new Vector3 (transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);

        if ((target - transform.position).magnitude > 0.1f) {
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotateSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp (transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
}