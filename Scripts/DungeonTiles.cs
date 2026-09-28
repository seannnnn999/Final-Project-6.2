using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTiles : MonoBehaviour
{

    public SpriteRenderer tile1_SpriteRenderer;
    public SpriteRenderer tile2_SpriteRenderer;
    public SpriteRenderer tile3_SpriteRenderer;

    public GameObject canvas;
    public GameObject tile_screen;

    public bool correctAnswer1 = false;
    public bool correctAnswer2 = false;
    public bool correctAnswer3 = false;
    public int currentTile = 0;

    void Start()
    {
        //tile1_SpriteRenderer = GetComponent<SpriteRenderer>();
        //tile2_SpriteRenderer = GetComponent<SpriteRenderer>();
        //tile3_SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (this.gameObject.tag == "Tile 1" && correctAnswer1 == true && currentTile == 1)
        {

            //dark red
            tile1_SpriteRenderer.color = new Color32(61, 13, 13, 255);
        }

        if (this.gameObject.tag == "Tile 2" && correctAnswer2 == true && currentTile == 2)
        {
            //dark yellow
            tile2_SpriteRenderer.color = new Color32(180, 160, 10, 255);
        }

        if (this.gameObject.tag == "Tile 3" && correctAnswer3 == true && currentTile == 3)
        {
            //dark green
            tile3_SpriteRenderer.color = new Color32(45, 50, 0, 255);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ((this.gameObject.tag == "Tile 1" && correctAnswer1 == false) ||
           (this.gameObject.tag == "Tile 2" && correctAnswer2 == false) ||
           (this.gameObject.tag == "Tile 3" && correctAnswer3 == false)))
        {
            canvas.SetActive(true);
            tile_screen.SetActive(true);
        }

        if (this.gameObject.tag == "Tile 1")
        {
            currentTile = 1;
        }

        if (this.gameObject.tag == "Tile 2")
        {
            currentTile = 2;
        }

        if (this.gameObject.tag == "Tile 3")
        {
            currentTile = 3;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        currentTile = 0;
    }
}
