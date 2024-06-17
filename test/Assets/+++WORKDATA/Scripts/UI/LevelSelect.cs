using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    //side note: i just noticed while working on this that Getactive scene +/- whatever means that its reffering 1 or 2 before or after the active scene right now, so the scenes in the build settings arent corelated to the order theyre put in there. like 1 isnt alyways 1 
    public void Level1()
    {
        SceneManager.LoadScene("Level1"); 
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
