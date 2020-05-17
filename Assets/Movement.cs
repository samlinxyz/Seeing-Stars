using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private enum Mode
    {
        Minecraft,
        MouseOnly
    }
    [SerializeField]
    private Rigidbody rigidBody = null;

    [SerializeField]
    private float speed = 1000f;

    float horizontalSpeed = 2.0f;
    float verticalSpeed = -2.0f;

    Vector2 initialMousePosition;
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    float eulerX = 0f;
    float eulerY = 0f;

    [SerializeField]
    private float acceleration = 1f;

    [SerializeField]
    private Mode mode = Mode.Minecraft;

    private void Update()
    {
        eulerY += horizontalSpeed * Input.GetAxis("Mouse X");
        eulerX += verticalSpeed * Input.GetAxis("Mouse Y");
        eulerX = Mathf.Clamp(eulerX, -85f, 85f);

        transform.rotation = Quaternion.Euler(eulerX, eulerY, 0f);

        //speed = Mathf.Pow(2f, Input.mouseScrollDelta.y);
        //float speedMultiplier = 1f + acceleration * ((Input.GetKey(KeyCode.Space) ? 1f : 0f) - (Input.GetKey(KeyCode.LeftShift) ? 1f : 0f)) * Time.deltaTime;
        //speed *= speedMultiplier;
        if (Input.GetMouseButtonDown(0))
        {
            speed *= acceleration;
        }
        if (Input.GetMouseButtonDown(1))
        {
            speed /= acceleration;
        }

        Vector3 direction = Vector3.zero;
        switch (mode)
        {
            case Mode.Minecraft:
                direction += Input.GetAxis("Horizontal") * Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
                direction += Input.GetAxis("Vertical") * Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
                direction += ((Input.GetKey(KeyCode.Space) ? 1f : 0f) - (Input.GetKey(KeyCode.LeftShift) ? 1f : 0f)) * Vector3.up;
                direction *= speed;
                break;
            case Mode.MouseOnly:
                direction = ((Input.GetMouseButton(0) ? 1f : 0f) - (Input.GetMouseButton(1) ? 1f : 0f)) * transform.forward;
                direction *= speed;
                break;
            default:
                break;
        }

        rigidBody.AddForce(direction * Time.deltaTime);
    }
}
