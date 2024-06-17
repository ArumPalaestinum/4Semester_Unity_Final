using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //use SceneManager to load scene Number 1 from the build
    }

    public void Levels()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); //same as play game but for scene 2
    }

    public void QuitGame()
    {
        Application.Quit(); //simply quit Application
    }
}
