using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    Protagonist playerScript;


    public GameObject player;

    public int health;
    public int power;

    public bool loveActive = false;
    public bool confusionActive = false;

    //public Nodes currentNode;
    //public List<Nodes> path = new List<Nodes>();

    
    private Rigidbody2D rb;       
    private SpriteRenderer spriteRenderer;  


    public int sleepiness = 0;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();      
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Protagonist>();


        if (this.gameObject.tag == "Enemy 11")
        {
            UnityEngine.Debug.Log(StaticValues.enemy1PositionX);
            this.transform.position = new Vector2(StaticValues.enemy1PositionX, StaticValues.enemy1PositionY);

            health = 10;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;
            }
            
        }

        if (this.gameObject.tag == "Enemy 12")
        {
            this.transform.position = new Vector2(StaticValues.enemy2PositionX, StaticValues.enemy2PositionY);

            health = 10;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;
            }

        }

        if (this.gameObject.tag == "Enemy 21")
        {
            this.transform.position = new Vector2(StaticValues.enemy3PositionX, StaticValues.enemy3PositionY);

            health = 20;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;
            }
        }

        if (this.gameObject.tag == "Enemy 22")
        {
            this.transform.position = new Vector2(StaticValues.enemy4PositionX, StaticValues.enemy4PositionY);

            health = 20;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;
            }
        }

        if (this.gameObject.tag == "Enemy 23")
        {
            this.transform.position = new Vector2(StaticValues.enemy5PositionX, StaticValues.enemy5PositionY);

            health = 20;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;
            }
        }
        if (this.gameObject.tag == "Enemy 31")
        {
            this.transform.position = new Vector2(StaticValues.enemy6PositionX, StaticValues.enemy6PositionY);

            health = 30;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;
            
            }
        }

        if (this.gameObject.tag == "Enemy 32")
        {
            this.transform.position = new Vector2(StaticValues.enemy7PositionX, StaticValues.enemy7PositionY);

            health = 30;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;

            }
        }

        if (this.gameObject.tag == "Enemy 33")
        {
            this.transform.position = new Vector2(StaticValues.enemy8PositionX, StaticValues.enemy8PositionY);

            health = 30;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;

            }
        }

        if (this.gameObject.tag == "Enemy 34")
        {
            this.transform.position = new Vector2(StaticValues.enemy9PositionX, StaticValues.enemy9PositionY);

            health = 30;
            if (playerScript.killCount > 0)
            {
                power = 5;
            }
            else
            {
                power = 1;

            }
        }

        if (this.gameObject.tag == "Boss 1")
        {
            health = 50;
            if (playerScript.killCount > 0)
            {
                power = 10;
            }
            else
            {
                power = 5;
            }
        }
        if (this.gameObject.tag == "Boss 2")
        {
            health = 100;
            if (playerScript.killCount > 0)
            {
                power = 15;
            }
            else
            {
                power = 7;
            }
        }
        if (this.gameObject.tag == "Boss 3")
        {
            health = 200;
            if (playerScript.killCount > 0)
            {
                power = 20;
            }
            else
            {
                power = 10;
            }
        }


    }

    private void Update()
    {
        

        if(sleepiness >= 10)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            playerScript.enemies_eliminated++;
        }


        if (health == 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            playerScript.killCount++;
            playerScript.enemies_eliminated++;
            Destroy(this.gameObject);
        }

        //if(confusionActive==false)
        //{
        //        CreatePath();
        //}





    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            Destroy(collision.gameObject);
            if (this.gameObject.tag != "Boss 1" || this.gameObject.tag != "Boss 2" || this.gameObject.tag != "Boss 3")
            {
                sleepiness += playerScript.magic;
            }
            
        }

        if (collision.gameObject.CompareTag("Attack") && this.gameObject.tag != "Enemy 2")
        {
            if(loveActive==false)
            {
                health -= playerScript.power;
            }
            
        }

        if (collision.gameObject.CompareTag("Player"))
        {

            playerScript.health -= power;
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(collision.gameObject);

            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }

        if (collision.gameObject.CompareTag("Oil Bottle"))
        {

            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            StartCoroutine(oilSlipCountdown());
        }

        if (collision.gameObject.CompareTag("Sleep Potion"))
        {
            sleepiness += 10;
        }

        if (collision.gameObject.CompareTag("Love Potion"))
        {
            
            loveActive = true;
            StartCoroutine(loveCountdown());
        }

        if (collision.gameObject.CompareTag("Confusion Potion"))
        {
            
            confusionActive = true;
            StartCoroutine(confusionCountdown());
        }
    }



    //public void CreatePath()
    //{
    //    if (path.Count > 0)
    //    {
    //        int x = 0;
    //        transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), 3 * Time.deltaTime);

    //        if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
    //        {
    //            currentNode = path[x];
    //            path.RemoveAt(x);
    //        }
    //    }
    //    else
    //    {
    //        Nodes[] nodes = FindObjectsOfType<Nodes>();

    //        while (path == null || path.Count == 0)
    //        {
    //            path = AStarAlgorithm.instance.GeneratePath(currentNode, endNode);
    //        }

    //    }
    //}


    IEnumerator oilSlipCountdown()
    {

        yield return new WaitForSeconds(3f);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }

    IEnumerator confusionCountdown()
    {

        yield return new WaitForSeconds(3f);
        loveActive = false; 

    }

    IEnumerator loveCountdown()
    {

        yield return new WaitForSeconds(3f);
        confusionActive = false;

    }
}
