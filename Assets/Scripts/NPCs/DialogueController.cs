using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public UnityAction endDialogue;
    public GameObject talkingPrefab;
    private GameObject currentUI;
    public GameObject cam;
    public EventRelay relay;
    [HideInInspector]
    public GameObject currentTalker;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTalker == null)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out var hit, 3f) && currentUI == null)
            {
                if (hit.collider.gameObject.GetComponent<NPCcontroller>() == null)
                    return;
                currentUI = Instantiate(hit.collider.gameObject.GetComponent<NPCcontroller>().currentDialogue, this.transform);
                currentTalker = hit.collider.gameObject;
            }
        }
        if (Input.GetKey(KeyCode.Escape))
            endDialogue?.Invoke();
    }

    public void SetAnimTrigger(string state)
    {
        if (currentTalker != null)
            currentTalker.GetComponent<NPCcontroller>().SetTrigger(state);
    }

    public void NextDialogue()
    {
        if (currentTalker != null)
            currentTalker.GetComponent<NPCcontroller>().NextDialogue();
    }

    public void DisableMovement()
    {
        cam.gameObject.GetComponentInParent<PlayerMovement>().playerControlled = false;
    }

    public void EnableMovement()
    {
        cam.gameObject.GetComponentInParent<PlayerMovement>().playerControlled = true;
    }
}
