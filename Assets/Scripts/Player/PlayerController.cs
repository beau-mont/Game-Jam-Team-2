using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Rotation")]
    public GameObject xRotator;
    public GameObject yRotator;
    public float sensitivity;

    [Header("Movement")]
    public float topSpeed;
    public float acceleration;
    


    private InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        
    }
}
