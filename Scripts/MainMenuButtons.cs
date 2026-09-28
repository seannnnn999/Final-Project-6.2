using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MainMenuButtons : MonoBehaviour
{

    public GameObject settings_screen;
    public GameObject main_screen;

    public void startGame()
    {
        SceneManager.LoadScene("Customisation Menu");
    }

    public void showSettings()
    {
        settings_screen.SetActive(true);
        main_screen.SetActive(false);
    }

    public void exitSettings()
    {
        settings_screen.SetActive(false);
        main_screen.SetActive(true);
    }

    public void exitGame()
    {
        UnityEngine.Application.Quit();
    }



}
