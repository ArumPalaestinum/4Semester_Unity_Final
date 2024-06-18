using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
   

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseGame(); //if escape is pressed activate pause game
        }
    }

    //load the main menu
    void PauseGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
