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
    [SerializeField] private int questIndex;
    [SerializeField] private GameObject QuestUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetQuest(quests.FirstOrDefault().QuestObject);
    }

    public void CompleteQuest(QuestObject quest)
    {
        if (!quests.Any(a => a.QuestObject == quest))
        {
            Debug.LogWarning($"quest controller is not assigned quest '{quest.Name}'");
            return;
        }
        foreach(var item in quests.Where(a => a.QuestObject == quest))
        {
            item.IsCompleted = true;
        }
    }

    public void SetQuest(QuestObject quest)
    {
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