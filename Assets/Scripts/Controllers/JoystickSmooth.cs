using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
public class JoystickSmooth : MonoBehaviour
{
    [EnumToggleButtons]
    [BoxGroup("ControllerType")]
    public ControllerType controllerType;

    [BoxGroup("Objects")]
    public DynamicJoystick joystick;
    [BoxGroup("Float Values")]
    public float moveSpeed = 10.0f;
    [ShowIf("controllerType", ControllerType.JOYSTICK)]
    [BoxGroup("Float Values")]
    public float rotateSpeed = 10.0f;
    [HideIf("controllerType", ControllerType.JOYSTICK)]
    [BoxGroup("Float Values")]
    public float range = 5f;
    void Start()
    {
        HUDPanel.LevelStart += FindJoystick;
    }
    void FindJoystick()
    {
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
    }


    void FixedUpdate()
    {
        if (GameManager.Instance.CheckState(GameManager.States.Play))
        {
            switch (controllerType)
            {
                case ControllerType.JOYSTICK:
                    MoveJoystick();
                    break;
                case ControllerType.DEFAULTDIRECTION:
                    MoveDefaultDirection();
                    break;
                case ControllerType.HORIZONTAL:
                    MoveHorinzontal();
                    break;
                case ControllerType.VERTICAL:
                    MoveVertical();
                    break;
            }
        }
    }

    void MoveJoystick()
    {
        Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;

        Vector3 target = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);

        if ((direction).magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
    void MoveDefaultDirection()
    {
        Vector3 target = Vector3.zero;
        if (joystick.Vertical > 0.5f && joystick.Horizontal < 0.5f && joystick.Horizontal > -0.5f)
        {
            // up
            target = new Vector3(transform.position.x, transform.position.y, range);
        }
        else if (joystick.Vertical < -0.5f && joystick.Horizontal < 0.5f && joystick.Horizontal > -0.5f)
        {
            // down
            target = new Vector3(transform.position.x, transform.position.y, -range);
        }
        else if (joystick.Horizontal > 0.5f && joystick.Vertical < 0.5f && joystick.Vertical > -0.5f)
        {
            // right
            target = new Vector3(range, transform.position.y, transform.position.z);
        }
        else if (joystick.Horizontal < -0.5f && joystick.Vertical < 0.5f && joystick.Vertical > -0.5f)
        {
            //left
            target = new Vector3(-range, transform.position.y, transform.position.z);
        }
        if (joystick.Horizontal != 0.0f && joystick.Vertical != 0.0f)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
    void MoveHorinzontal()
    {
        Vector3 target = transform.position + joystick.Horizontal * Vector3.right;
        target.x = Mathf.Clamp(target.x, -range, range);
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

    }
    void MoveVertical()
    {
        Vector3 target = transform.position + joystick.Vertical * Vector3.forward;
        target.z = Mathf.Clamp(target.z, -range, range);
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}