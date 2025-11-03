using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechHelper
{
    
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> DialogueLines { get; set; }
    public float currentLine { get; set; }
    public float HangTime { get; set; }
}

[System.Serializable]
public class DialogueLine
{
    public string Line { get; set; }
    public float Delay { get; set; }
    public float Speed { get; set; }
    public float HangTime { get; set; }
    public DialogueSound Sound { get; set; }
}

[System.Serializable]
public class DialogueSound
{
    public AudioClip Clip { get; set; }
    public float Delay { get; set; }
}

