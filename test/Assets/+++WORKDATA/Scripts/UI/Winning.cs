using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");//load main menu
    }

    public void QuitGame()
    {
        Application.Quit(); // quit Application
    }

}
