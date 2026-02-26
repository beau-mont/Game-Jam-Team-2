using UnityEngine;
using UnityEngine.InputSystem;

public class OpenMinigame : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Assign the UI Canvas")]
    public GameObject uiCanvas;

    [Tooltip("Assign player or camera transform")]
    public Transform player;

    [Header("Settings")]
    [Tooltip("How close the player must be to interact")]
    public float interactDistance = 3f;

    [Header("Input")]
    [Tooltip("Reference to Interact InputAction (bound to E key)")]
    public InputActionReference interactAction;

    private bool isPlayerNear = false;
    private bool isUIVisible = false;

    private void Start()
    {
        if (uiCanvas != null)
            uiCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        interactAction.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            isPlayerNear = distance <= interactDistance;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (!isPlayerNear) return;

        isUIVisible = !isUIVisible;
        uiCanvas.SetActive(isUIVisible);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}
