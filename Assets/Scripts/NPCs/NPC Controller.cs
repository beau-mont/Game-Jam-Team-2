using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCcontroller : MonoBehaviour
{
    public GameObject currentDialogue;
    public GameObject cam;
    private int ticker;
    public List<GameObject> dialogueList;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ticker = 0;
        currentDialogue = dialogueList.FirstOrDefault();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(cam.transform.position, Vector3.up);
    }

    public void NextDialogue()
    {
        ticker++;
        if (dialogueList.Count > ticker)
            currentDialogue = dialogueList[ticker];
        else
            Debug.Log("not enough dialogue specified");
    }
    
    public void SetTrigger(string state)
    {
        anim.SetTrigger(state);
    }
}
