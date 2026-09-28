using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuButtons : MonoBehaviour
{
    public GameObject[] skins;
    public GameObject[] black_hairs;
    public GameObject[] brown_hairs;
    public GameObject[] ginger_hairs;
    public GameObject[] platinum_hairs;

    public int currentSkin;
    public int newSkin;

    public int currentHairType;
    public int newHairType;

    public int currentHairColor;

    public GameObject hut_screen;
    public GameObject castle_screen;
    public GameObject customisation_screen;


    void Start()
    {

        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].activeSelf == true)
            {
                currentSkin = i;
            }
        }

        for (int i = 0; i < black_hairs.Length; i++)
        {
            if (black_hairs[i].activeSelf == true)
            {
                currentHairType = i;
            }
        }

        currentHairColor = 0;


        StartCoroutine(ShowCastleScreen());

        StartCoroutine(ShowCustomisationScreen());


    }


    void Update()
    {
        

    }


    public void switchSkin()
    {
        skins[currentSkin].SetActive(false);

        if(currentSkin == 7)
        {
            currentSkin = -1;
        }
        currentSkin++;

        skins[currentSkin].SetActive(true);
    }


    public void switchHairType()
    {
        if (currentHairType == 5)
        {
            newHairType = -1;
        }
        newHairType++;

        if (currentHairColor == 0)
        {
            black_hairs[currentHairType].SetActive(false);
            black_hairs[newHairType].SetActive(true);
        }

        if (currentHairColor == 1)
        {
            brown_hairs[currentHairType].SetActive(false);
            brown_hairs[newHairType].SetActive(true);
        }

        if (currentHairColor == 2)
        {
            ginger_hairs[currentHairType].SetActive(false);
            ginger_hairs[newHairType].SetActive(true);
        }

        if (currentHairColor == 3)
        {
            platinum_hairs[currentHairType].SetActive(false);
            platinum_hairs[newHairType].SetActive(true);
        }

        currentHairType = newHairType;
    }


    public void switchHairColor()
    {
        if(currentHairColor == 0)
        {
            black_hairs[currentHairType].SetActive(false);
            brown_hairs[currentHairType].SetActive(true);
        }

        if (currentHairColor == 1)
        {
            brown_hairs[currentHairType].SetActive(false);
            ginger_hairs[currentHairType].SetActive(true);
        }

        if (currentHairColor == 2)
        {
            ginger_hairs[currentHairType].SetActive(false);
            platinum_hairs[currentHairType].SetActive(true);
        }

        if (currentHairColor == 3)
        {
            platinum_hairs[currentHairType].SetActive(false);
            black_hairs[currentHairType].SetActive(true);
        }

        if (currentHairColor == 3)
        {
            currentHairColor = 0;
        }
        else
        {
            currentHairColor++;
        }
    }

    public void loadNextScene()
    {
        StaticValues.skin = currentSkin;
        StaticValues.hairType = currentHairType;
        StaticValues.hairColor = currentHairColor;
        SceneManager.LoadScene("Level 1");
    }


    IEnumerator ShowCastleScreen()
    {
        
        yield return new WaitForSeconds(3f);
        hut_screen.SetActive(false);
        castle_screen.SetActive(true);

    }

    IEnumerator ShowCustomisationScreen()
    {
        yield return new WaitForSeconds(8f);
        castle_screen.SetActive(false);
        customisation_screen.SetActive(true);
    }


}
