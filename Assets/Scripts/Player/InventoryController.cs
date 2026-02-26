using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting.ReorderableList;
using UnityEngine.Events;
using UnityEditor;

public class InventoryController : MonoBehaviour
{
    // doing this in the simplest way possible because i spent way too much time on other code
    public int stickCount;
    private QuestController questController;
    public GameObject stickCounter;
    public List<GameObject> UIElements;
    public GameObject cam;
    private bool visible;
    public float hideDelay;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questController = FindFirstObjectByType<QuestController>();
        foreach (var item in UIElements)
        {
            if (item.TryGetComponent<Image>(out var i))
                i.CrossFadeAlpha(0f, 0f, true);
            if (item.TryGetComponent<TextMeshProUGUI>(out var t))
                t.CrossFadeAlpha(0f, 0f, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (questController.questIndex >= questController.quests.Count || questController.questIndex < 0) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out var hit, 3f)) // i know im running three raycasts at once to do very similar things but i need this done fast lol
            {
                if (questController.quests[questController.questIndex].QuestObject.Name == "Campfire Quest") // hardcoded but heeheee
                {
                    if (hit.collider.gameObject.CompareTag("Stick"))
                    {
                        stickCount++;
                        ShowCount();
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.gameObject.GetComponent<Campfire>() == null)
                        return;
                    if (hit.collider.gameObject.CompareTag("Campfire") && hit.collider.GetComponent<Campfire>().sticksNeeded <= stickCount)
                    {
                        hit.collider.GetComponent<Campfire>().StartFire();
                        stickCount = 0;
                        ShowCount();
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
            ShowCount();
        if (timer <= Time.time)
            visible = false;
        if (!visible)
        {
            foreach (var item in UIElements)
            {
                if (item.TryGetComponent<Image>(out var i))
                    i.CrossFadeAlpha(0f, 0.25f, true);
                if (item.TryGetComponent<TextMeshProUGUI>(out var t))
                    t.CrossFadeAlpha(0f, 0.25f, true);
            }
        }
    }

    public void ShowCount()
    {
        stickCounter.GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
        stickCounter.GetComponent<TextMeshProUGUI>().text = stickCount.ToString();
        visible = true;
        timer = Time.time + hideDelay;
        foreach (var item in UIElements)
        {
            if (item.TryGetComponent<Image>(out var i))
                i.CrossFadeAlpha(1f, 0.5f, true);
            if (item.TryGetComponent<TextMeshProUGUI>(out var t))
                t.CrossFadeAlpha(1f, 0.5f, true);
        }
    }
}