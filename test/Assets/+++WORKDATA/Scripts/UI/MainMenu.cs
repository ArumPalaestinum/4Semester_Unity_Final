using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1"); //use SceneManager to load scene Number 1 from the build
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels"); //same as play game but for scene 2
    }

    public void QuitGame()
    {
        Application.Quit(); //simply quit Application
    }
}
