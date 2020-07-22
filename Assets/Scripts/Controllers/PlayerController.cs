using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerController : MonoBehaviour
{
    public enum ControllerType
    {
        JOYSTICK,
        DEFAULTDIRECTION,
        HORIZONTAL,
        VERTICAL,
    }

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
    public float range = 10f;
    private void Start()
    {
        HUDPanel.FindJoyStick += FindJoystick;
    }
    public void FindJoystick()
    {
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
    }
    private void OnDestroy()
    {
        HUDPanel.FindJoyStick -= FindJoystick;
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

        if ((target - transform.position).magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
    void MoveDefaultDirection()
    {
        if (joystick.Vertical > 0.5f && joystick.Horizontal < 0.5f && joystick.Horizontal > -0.5f)
        {
            // up
            Vector3 target = new Vector3(transform.position.x, transform.position.y, range);
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else if (joystick.Vertical < -0.5f && joystick.Horizontal < 0.5f && joystick.Horizontal > -0.5f)
        {
            // down
            Vector3 target = new Vector3(transform.position.x, transform.position.y, -range);
            transform.LookAt(target);

            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else if (joystick.Horizontal > 0.5f && joystick.Vertical < 0.5f && joystick.Vertical > -0.5f)
        {
            // right
            Vector3 target = new Vector3(range, transform.position.y, transform.position.z);
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else if (joystick.Horizontal < -0.5f && joystick.Vertical < 0.5f && joystick.Vertical > -0.5f)
        {
            //left
            Vector3 target = new Vector3(-range, transform.position.y, transform.position.z);
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
    void MoveHorinzontal()
    {
        if (joystick.Horizontal > 0.4f)
        {
            Vector3 target = new Vector3(range, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else if (joystick.Horizontal < -0.4f)
        {
            Vector3 target = new Vector3(-range, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

    }
    void MoveVertical()
    {
        if (joystick.Vertical > 0.4f)
        {
            Vector3 target = new Vector3(transform.position.x, transform.position.y, range);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else if (joystick.Vertical < -0.4f)
        {
            Vector3 target = new Vector3(transform.position.x, transform.position.y, -range);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
}