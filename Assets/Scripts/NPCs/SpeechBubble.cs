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

public class SpeechBubble : MonoBehaviour
{
    [Header("Setup")]
    public TextMeshProUGUI textMesh;
    public GameObject MainObject;
    public AudioSource audioSource;
    private float timer;
    private DialogueController dCont;
    private float destroyAt;
    private float startAt;

    [Header("Dialogue Settings")]
    public Dialogue dialogue;
    public bool speaking = false;
    public float animationTime = 1f;
    private bool nextLine = false;

    void Start()
    {
        dCont = FindFirstObjectByType<DialogueController>();
        dCont.endDialogue += EndDialogue;
        StartDialogue();
    }

    #region events

    void StartDialogue()
    {
        dialogue.CurrentLine = 0;
        nextLine = true;
        Image[] imageObjs = MainObject.GetComponentsInChildren<Image>();
        TextMeshProUGUI[] textObjs = MainObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var item in imageObjs)
        {
            item.CrossFadeAlpha(0f, 0f, true);
            item.CrossFadeAlpha(1f, animationTime, false);
        }
        foreach (var item in textObjs)
        {
            item.CrossFadeAlpha(0f, 0f, true);
            item.CrossFadeAlpha(1f, animationTime, false);
        }
        startAt = Time.time + animationTime;
        dCont.DisableMovement();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(QueueStart());
    }

    private IEnumerator QueueStart()
    {
        while (true)
        {
            if (Time.time >= startAt)
                speaking = true;
            yield return new WaitForEndOfFrame();
        }
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        speaking = false;
        dCont.endDialogue -= EndDialogue;
        dCont.currentTalker = null;
        Image[] imageObjs = MainObject.GetComponentsInChildren<Image>();
        TextMeshProUGUI[] textObjs = MainObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var item in imageObjs)
            item.CrossFadeAlpha(0f, animationTime, false);
        foreach (var item in textObjs)
            item.CrossFadeAlpha(0f, animationTime, false);
        destroyAt = Time.time + animationTime;
        dCont.EnableMovement();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(QueueDestroy());
    }

    private IEnumerator QueueDestroy()
    {
        while (true)
        {
            if (Time.time >= destroyAt)
                Destroy(MainObject);
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion

    void Update()
    {
        if (Time.time >= timer && speaking)
        {
            if (dialogue.CurrentLine < dialogue.DialogueLines.Count)
            {
                timer = Time.time + dialogue.DialogueLines[dialogue.CurrentLine].WaitTime + dialogue.DialogueLines[dialogue.CurrentLine].SpeechTime;
                if (audioSource != null && dialogue.DialogueLines[dialogue.CurrentLine].Sound != null)
                {
                    audioSource.clip = dialogue.DialogueLines[dialogue.CurrentLine].Sound;
                    audioSource.Play();
                }
            }
            EndCheck();
        }
    }

    void EndCheck()
    {
        if (dialogue.CurrentLine < dialogue.DialogueLines.Count && speaking)
        {
            textMesh.text = dialogue.DialogueLines[dialogue.CurrentLine].Line;
            StartCoroutine(TextTypewriter());
        }
        else
            EndDialogue();
    }

    private IEnumerator TextTypewriter() // taken from a tutorial and slightly modified but i cant find the tutorial to link it now lol
    {
        if (dialogue.CurrentLine > dialogue.DialogueLines.Count)
            StopCoroutine(TextTypewriter());
        if (nextLine)
        {
            dialogue.DialogueLines[dialogue.CurrentLine].Event?.Invoke();
            nextLine = false;
        }
        textMesh.ForceMeshUpdate();
        float t = dialogue.DialogueLines[dialogue.CurrentLine].SpeechTime / dialogue.DialogueLines[dialogue.CurrentLine].Line.Length;
        int step = textMesh.textInfo.characterCount;
        int letter = 0;
        while (true)
        {
            int visCount = letter % (step + 1);
            textMesh.maxVisibleCharacters = visCount;
            if (visCount >= step)
            {
                nextLine = true;
                dialogue.CurrentLine++;
                break;
            }
            letter++;
            yield return new WaitForSeconds(t);
        }
    }
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> DialogueLines;
    public int CurrentLine;
}

[System.Serializable]
public class DialogueLine
{
    public string Line;
    public float SpeechTime;
    public float WaitTime;
    public AudioClip Sound;
    public UnityEvent Event;
}