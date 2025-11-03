using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public List<string> lines;
    public List<AudioClip> speechSounds;
    public float speechTime;
    int i = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EndCheck();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EndCheck()
    {
        if (i <= lines.Count - 1)
        {
            textMesh.text = lines[i];
            StartCoroutine(TextTypewriter());
        }
    }

    
    private IEnumerator TextTypewriter()
    {
        textMesh.ForceMeshUpdate();
        float t = speechTime / lines[0].Length;
        int step = textMesh.textInfo.characterCount;
        int ticker = 0;
        while (true)
        {
            int visCount = ticker % (step + 1);
            textMesh.maxVisibleCharacters = visCount;
            if (visCount >= step)
            {
                i++;
                Invoke("EndCheck", t);
                break;
            }
            ticker++;
            yield return new WaitForSeconds(t);
        }
    }
}
