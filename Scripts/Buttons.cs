using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    Scene currentScene;
    string currentSceneName;

    Protagonist playerScript;
    public GameObject player;

    DungeonTiles tile1Script, tile2Script, tile3Script;
    public GameObject tile1, tile2, tile3;

    Interactable interactableScript;
    public GameObject interactables;

    public GameObject canvas;


    public GameObject stats_screen;
    public GameObject inventory_screen;
    public GameObject recipe_screen;

    public GameObject tiles_screen;
    public int tilesCollected = 0;

    public GameObject lever_screen;
    public GameObject chest_screen;
    public GameObject potionsTable_screen;
    public GameObject firstPotion_screen;
    public GameObject secondPotion_screen;
    public GameObject finalPotion_screen;

    public GameObject chest1_screen;
    public GameObject chest2_screen;
    public GameObject chest3_screen;
    public GameObject chest4_screen;

    public GameObject bossLevel;




    void Start()
    {

        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;



        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Protagonist>();


        if (currentSceneName == "Level 1" && bossLevel.activeSelf == true)
        {
            tile1 = GameObject.FindGameObjectWithTag("Tile 1");
            tile1Script = tile1.GetComponent<DungeonTiles>();

            tile2 = GameObject.FindGameObjectWithTag("Tile 2");
            tile2Script = tile2.GetComponent<DungeonTiles>();

            tile3 = GameObject.FindGameObjectWithTag("Tile 3");
            tile3Script = tile3.GetComponent<DungeonTiles>();
        }

        interactables = GameObject.FindGameObjectWithTag("Quest Manager");
        interactableScript = interactables.GetComponent<Interactable>();



    }

    void Update()
    {
        if(tilesCollected == 3)
        {
            interactableScript.key.SetActive(true);
        }
    }

    


    public void Inventory()
    {
        stats_screen.SetActive(!gameObject.activeSelf);
        inventory_screen.SetActive(!gameObject.activeSelf);
    }

    public void Tile1()
    {
        if(tile1.gameObject.tag == "Tile 1" && tile1Script.currentTile == 1)
        {
            tile1Script.correctAnswer1 = true;
            tilesCollected++;
        }
        canvas.SetActive(!gameObject.activeSelf);
        tiles_screen.SetActive(!gameObject.activeSelf);
    }

    public void Tile2()
    {
        if (tile2.gameObject.tag == "Tile 2" && tile2Script.currentTile == 2)
        {
            tile2Script.correctAnswer2 = true;
            tilesCollected++;
        }
        canvas.SetActive(!gameObject.activeSelf);
        tiles_screen.SetActive(!gameObject.activeSelf);
    }

    public void Tile3()
    {
        if (tile3.gameObject.tag == "Tile 3" && tile3Script.currentTile == 3)
        {
            tile3Script.correctAnswer3 = true;
            tilesCollected++;
        }
        canvas.SetActive(!gameObject.activeSelf);
        tiles_screen.SetActive(!gameObject.activeSelf);
    }

    public void exitScreen()
    {
        canvas.SetActive(false);
        tiles_screen.SetActive(false);
    }

    public void exitChestScreen()
    {
        canvas.SetActive(false);
        chest_screen.SetActive(false);
    }

    public void exitRecipeScreen()
    {
        canvas.SetActive(false);
        recipe_screen.SetActive(false);
    }

    public void exitPotionsTableScreen()
    {
        canvas.SetActive(false);
        potionsTable_screen.SetActive(false);
    }


    public void pulledLever()
    {
        interactableScript.ball.SetActive(true);
        canvas.SetActive(!gameObject.activeSelf);
        lever_screen.SetActive(!gameObject.activeSelf);
    }

    public void firstPotion()
    {
        firstPotion_screen.SetActive(false);
        secondPotion_screen.SetActive(true);
    }

    public void secondPotion()
    {
        secondPotion_screen.SetActive(false);
        finalPotion_screen.SetActive(true);
    }

    public void finalPotion()
    {
        interactableScript.cauldron.SetActive(false);
        interactableScript.explosion_potion.SetActive(true);
        canvas.SetActive(false);
        finalPotion_screen.SetActive(false);
    }

    public void wrongFirstPotion()
    {
        canvas.SetActive(!gameObject.activeSelf);
        firstPotion_screen.SetActive(!gameObject.activeSelf);
    }

    public void wrongSecondPotion()
    {
        canvas.SetActive(!gameObject.activeSelf);
        secondPotion_screen.SetActive(!gameObject.activeSelf);
    }

    public void wrongFinalPotion()
    {
        canvas.SetActive(!gameObject.activeSelf);
        finalPotion_screen.SetActive(!gameObject.activeSelf);
    }

    public void exitSleepPotion()
    {
        playerScript.sleep_potions += 1;
        canvas.SetActive(false);
        chest1_screen.SetActive(false);
    }

    public void exitLovePotion()
    {
        playerScript.love_potions += 1;
        canvas.SetActive(false);
        chest2_screen.SetActive(false);
    }

    public void exitConfusionPotion()
    {
        playerScript.confusion_potions += 1;
        canvas.SetActive(false);
        chest3_screen.SetActive(false);
    }

    public void exitUpgrade()
    {

        playerScript.magic += 5;
        canvas.SetActive(false);
        chest4_screen.SetActive(false);
    }

}
