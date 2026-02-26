using UnityEngine;
using UnityEngine.Events;

public class EventRelay : MonoBehaviour
{
    public DialogueController dialogueController;
    public QuestController questController;
    void Start()
    {
        dialogueController = FindFirstObjectByType<DialogueController>();
        questController = FindFirstObjectByType<QuestController>();
    }

    public void RelayAnimTrigger(string state)
    {
        dialogueController.SetAnimTrigger(state);
    }

    public void RelayNextDialogue()
    {
        dialogueController.NextDialogue();
    }

    public void SetQuest(QuestObject quest)
    {
        questController.SetQuest(quest);
    }

    public void CompleteQuest(QuestObject quest)
    {
        questController.CompleteQuest(quest);
    }
}
