using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ScoreBoard()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 8);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
