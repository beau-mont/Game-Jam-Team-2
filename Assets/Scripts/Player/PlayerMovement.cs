using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    InputAction moveAction;
    public float walkSpeed = 5f;
    Rigidbody rb;
    [Header("Rotation")]
    public float xSensitivity = 2f;
    public float ySensitivity = 0.05f;
    public float upperClamp = 80f;
    public float lowerClamp = -80f;
    public GameObject xRotator;
    public GameObject yRotator;
    private Vector2 mouseInput = Vector2.zero;
    public bool playerControlled;
    public bool animatorControlled;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerControlled)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            Vector3 moveDirection = transform.TransformDirection(new Vector3(moveValue.x, 0f, moveValue.y)).normalized;

            rb.AddForce((moveDirection * walkSpeed) - rb.linearVelocity);

            if (xRotator != null && yRotator != null)
            {
                mouseInput.y += Input.GetAxis("Mouse X");
                mouseInput.x -= Input.GetAxis("Mouse Y");

                mouseInput.x = Mathf.Clamp(mouseInput.x, lowerClamp / 2f, upperClamp / 2f);
            }
        }
        xRotator.transform.localRotation = Quaternion.Euler(mouseInput.x * xSensitivity, 0f, 0f);
        yRotator.transform.localRotation = quaternion.Euler(0f, mouseInput.y * ySensitivity, 0f);
        if (animatorControlled)
            GetComponent<Animator>().enabled = true;
        else
            GetComponent<Animator>().enabled = false;
    }
}
