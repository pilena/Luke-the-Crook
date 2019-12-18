using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("You have quit the game!");
        Application.Quit();
    }

    public void LoadAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
