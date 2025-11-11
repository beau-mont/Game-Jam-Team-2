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

public class FoxfireMenu : MonoBehaviour
{
    public GameObject player;
    private Animator playerAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            playerAnim.SetTrigger("StartGame");
            GetComponent<Image>().CrossFadeAlpha(0f, 2f, false);
        }
    }
}
