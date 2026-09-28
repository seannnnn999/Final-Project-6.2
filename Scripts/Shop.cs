using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    Scene currentScene;
    string currentSceneName;

    Buttons buttonsScript;
    public GameObject buttons;

    public GameObject questCompletedScreen;
    public GameObject questNOTCompletedScreen;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        buttons = GameObject.FindGameObjectWithTag("Button Manager");
        buttonsScript = buttons.GetComponent<Buttons>();

        canvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        //if (SceneManager.GetActiveScene().name == "Level 1")
        //{

        //}

        if (currentSceneName == "Level 1")
        {
            if(buttonsScript.tilesCollected == 3)
            {
                questCompletedScreen.SetActive(true);
            }
            else
            {
                questNOTCompletedScreen.SetActive(true);
            }
        }
    }
}
