using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.SetActive(false);  // Menu is hidden initially
        Time.timeScale = 1f;  // Ensure the game is not paused at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the menu on/off with Tab key
            bool isMenuActive = !menuCanvas.activeSelf;
            menuCanvas.SetActive(isMenuActive);

            // Pause the game when the menu is active, unpause when it's hidden
            Time.timeScale = isMenuActive ? 0f : 1f;
        }
    }
}
