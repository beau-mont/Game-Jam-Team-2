using UnityEngine;
using UnityEngine.Events;

public class EventRelay : ScriptableObject
{
    public DialogueController dCont;
    void Start()
    {
        dCont = FindFirstObjectByType<DialogueController>();
    }

    public void RelayAnimTrigger(string state)
    {
        dCont.SetAnimTrigger(state);
    }

    public void RelayNextDialogue()
    {
        dCont.NextDialogue();
    }
}
