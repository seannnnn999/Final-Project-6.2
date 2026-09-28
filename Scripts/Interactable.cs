using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Protagonist playerScript;
    public GameObject player;

    public GameObject box;

    public GameObject canvas;
    public GameObject note_screen;
    public GameObject chest_screen;
    public GameObject shop_screen;
    public GameObject nothing_screen;
    public GameObject lever_screen;
    public GameObject potionsTable_screen;
    public GameObject cauldron_screen;

    public GameObject key;
    public GameObject ball;
    public GameObject explosion_potion;
    public GameObject cauldron;

    public GameObject door;
    public GameObject chest;

    
    
    public bool doorArea = false;
    public bool chestArea = false;

    public bool noteArea = false;
    public bool deskArea = false;
    public bool bedArea = false;
    public bool nightstandArea = false;
    public bool leverArea = false;
    public bool cauldronArea = false;
    public bool potionsTableArea = false;

    public bool keyArea = false;
    public bool ballArea = false;
    public bool explosionPotionArea = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Protagonist>();
    }

    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            if(noteArea == true)
            {
                canvas.SetActive(true);
                note_screen.SetActive(true);
            }

            if(keyArea == true)
            {
                key.SetActive(false);
            }

            if (chestArea == true)
            {
                canvas.SetActive(true);
                chest_screen.SetActive(true);
            }

            if (doorArea == true)
            {
                canvas.SetActive(true);
                shop_screen.SetActive(true);
            }

            if (deskArea == true || bedArea == true || nightstandArea == true)
            {
                canvas.SetActive(true);
                nothing_screen.SetActive(true);
            }

            if (leverArea == true)
            {
                canvas.SetActive(true);
                lever_screen.SetActive(true);
            }

            if (ballArea == true)
            {
                ball.SetActive(false);
            }

            if (explosionPotionArea == true)
            {
                explosion_potion.SetActive(false);
            }

            if (potionsTableArea == true)
            {
                canvas.SetActive(true);
                potionsTable_screen.SetActive(true);
            }

            if (cauldronArea == true)
            {
                canvas.SetActive(true);
                cauldron_screen.SetActive(true);
            }

        }

        if(key.activeSelf == true || ball.activeSelf == true || explosion_potion.activeSelf == true)
        {
            door.SetActive(true);
            chest.SetActive(true);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.tag == "OilBottle")
        {
            playerScript.oil += 1;
            GameObject delete = GameObject.FindGameObjectWithTag("OilBottle");
            Destroy(delete.gameObject);
        }


        if (collision.gameObject.CompareTag("Player") && this.tag == "Potion")
        {
            box.SetActive(!box.activeSelf);
            if (Input.GetKeyDown(KeyCode.Y))
            {
                playerScript.power += 5;
                GameObject delete = GameObject.FindGameObjectWithTag("Potion");
                box.SetActive(!gameObject.activeSelf);
                Destroy(delete.gameObject);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                box.SetActive(!gameObject.activeSelf);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.tag == "Kitchen Note")
        {
            noteArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Key")
        {
            keyArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Chest")
        {
            chestArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Quest Door")
        {
            doorArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Desk")
        {
            deskArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Bed")
        {
            bedArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Nightstand")
        {
            nightstandArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Lever")
        {
            leverArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Ball")
        {
            ballArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Ball")
        {
            ballArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Ball")
        {
            ballArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Explosion Potion")
        {
            explosionPotionArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Cauldron")
        {
            cauldronArea = true;
        }

        if (collision.gameObject.CompareTag("Player") && this.tag == "Potions table")
        {
            potionsTableArea = true;
        }

}


    void OnCollisionExit2D(Collision2D collision)
    {
        noteArea = false;
        keyArea = false;
        chestArea = false;
        doorArea = false;
        deskArea = false;
        bedArea = false;
        nightstandArea = false;
        leverArea = false;
        ballArea = false;
        explosionPotionArea = false;
        cauldronArea = false;
        potionsTableArea = false;
    }

}
