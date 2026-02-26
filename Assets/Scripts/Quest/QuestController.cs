using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<Quest> quests;
    [SerializeField] private GameObject UICanvas;
    public int questIndex;
    [SerializeField] private GameObject QuestUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // SetQuest(quests.FirstOrDefault().QuestObject);
    }

    public void CompleteQuest(QuestObject quest)
    {
        if (!quest && questIndex >= 0)
        {
            quests[questIndex].IsCompleted = true;
            SetQuest(null);
            return;
        }
        if (!quests.Any(a => a.QuestObject == quest))
        {
            Debug.LogWarning($"quest controller is not assigned quest '{quest.Name}'");
            return;
        }
        // if (quests[questIndex].QuestObject != quest)
        // {
        //     Debug.LogWarning("attempted to complete quest that is not current quest");
        //     return;
        // }
        if (quests.FirstOrDefault().IsCompleted)
        {
            Debug.LogWarning("attempted to complete quest that is already completed");
            return;
        }
        quests.FirstOrDefault(a => a.QuestObject == quest).IsCompleted = true;
        SetQuest(null);
    }

    public void SetQuest(QuestObject quest)
    {
        if (!quest)
        {
            if (QuestUI)
                Destroy(QuestUI);
            questIndex = -1;
            return;
        }
        if (!quests.Any(a => a.QuestObject == quest))
        {
            Debug.LogWarning($"quest controller is not assigned quest '{quest.Name}'");
            return;
        }
        if (questIndex == quests.IndexOf(quests.FirstOrDefault(a => a.QuestObject == quest)))
        {
            Debug.LogWarning("attempted setting quest to current quest");
            return;
        }
        questIndex = quests.IndexOf(quests.FirstOrDefault(a => a.QuestObject == quest));
        if (quests[questIndex].IsCompleted)
        {
            Debug.LogWarning("attempted to set current quest to a completed quest");
            return;
        }
        if (QuestUI)
            Destroy(QuestUI);
        QuestUI = Instantiate(quest.QuestUI, UICanvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Quest
{
    public QuestObject QuestObject;
    public bool IsCompleted;
}