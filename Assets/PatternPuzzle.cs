using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternPuzzle : MonoBehaviour
{
    public Image image;
    public Sprite newImageSprite;

    [Header("Assign the buttons here")]
    public Button button1;
    public Button button2;
    public Button button3;

    [Header("Correct pattern")]
    public List<int> correctPattern = new List<int> { 2, 3, 1 };

    private List<int> playerInput = new List<int>();

    void Start()
    {
        button1.onClick.AddListener(() => OnButtonPressed(1));
        button2.onClick.AddListener(() => OnButtonPressed(2));
        button3.onClick.AddListener(() => OnButtonPressed(3));
    }

    void OnButtonPressed(int buttonNumber)
    {
        playerInput.Add(buttonNumber);
        Debug.Log($"Pressed Button {buttonNumber}");

        if (playerInput.Count == correctPattern.Count)
        {
            if (IsCorrectPattern())
            {
                PuzzleCompleted();
            }
            else
            {
                Debug.Log("Wrong pattern, you stupit bitc-");
                playerInput.Clear(); // reset
            }
        }
    }

    bool IsCorrectPattern()
    {
        for (int i = 0; i < correctPattern.Count; i++)
        {
            if (playerInput[i] != correctPattern[i])
                return false;
        }
        return true;
    }

    void PuzzleCompleted()
    {
        Debug.Log("Correct pattern. Congrats, you aren't dumber than a newborn");
        ChangeImage();
    }

    public void ChangeImage()
    {
        if (image != null && newImageSprite != null)
        {
            image.sprite = newImageSprite;
        }
    }
}
