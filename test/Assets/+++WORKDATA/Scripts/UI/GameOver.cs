using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Levels()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); //cheated a bit and just let the player choose the level they want to play again

    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }
}
