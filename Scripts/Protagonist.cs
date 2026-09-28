using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Protagonist : MonoBehaviour
{
  
    public float moveSpeed = 5f;

    
    private Rigidbody2D rb;        
    private SpriteRenderer spriteRenderer;  

    
    private float verticalInput;
    private float horizontalInput;  
    private bool isFacingRight = true;  

    public GameObject test;
    public GameObject spellPrefab;
    public GameObject spell;

    bool active = false;
    private Transform target;
    float step;

    public int oil = 0;
    public int sleep_potions = 0;
    public int love_potions = 0;
    public int confusion_potions = 0;

    public GameObject oilBottlePrefab;
    public GameObject oilBottle;

    public GameObject chest;

    public int power = 1;
    public int health = 100;
    public int intelligence = 5;
    public int kindness = 5;
    public int charisma = 5;
    public int magic = 5;
    public int killCount = 0;

    public Sprite[] skins;
    public Sprite[] hairs;

    public GameObject hair;

    public Animator playerAnimator;

    public GameObject wooden_spear;
    public GameObject dagger;
    public GameObject sword;

    private Vector2 movement;

    public AnimationClip[] animations;

    Scene currentScene;
    string currentSceneName;

    AbstractDungeonGenerator generator;

    public int roomRefreshed = 0;

    public GameObject canvas;
    public GameObject chest1_screen;
    public GameObject chest2_screen;
    public GameObject chest3_screen;
    public GameObject chest4_screen;

    public int enemies_eliminated = 0;

    void Start()
    {
        this.transform.position = new Vector2(StaticValues.playerPositionX, StaticValues.playerPositionY);

        chest.transform.position = new Vector2(StaticValues.chestPositionX, StaticValues.chestPositionY);

        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;


        rb = GetComponent<Rigidbody2D>();       
        spriteRenderer = this.GetComponent<SpriteRenderer>(); 

        this.GetComponent<SpriteRenderer>().sprite = skins[StaticValues.skin];
        hair.GetComponent<SpriteRenderer>().sprite = hairs[StaticValues.hairType*4 + StaticValues.hairColor];

        killCount = StaticValues.killCount;

        if(currentSceneName == "Level 1")
        {
            wooden_spear.SetActive(true);
        }

        if (StaticValues.weapon == 2)
        {
            wooden_spear.SetActive(false);
            dagger.SetActive(true);
            power = 5;
        }

        if (StaticValues.weapon == 3)
        {
            dagger.SetActive(false);
            sword.SetActive(true);
            power = 10;
        }

    }

    void Update()
    {

        if(roomRefreshed == 1)
        {
            this.transform.position = new Vector2(StaticValues.playerPositionX, StaticValues.playerPositionY);
            chest.transform.position = new Vector2(StaticValues.chestPositionX, StaticValues.chestPositionY);
            roomRefreshed = 0;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        movement.x = horizontalInput * moveSpeed * Time.deltaTime;
        movement.y = verticalInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        if (horizontalInput != 0 || verticalInput != 0)
        {
                playerAnimator.enabled = true;
                if (StaticValues.skin == 0)
            {
                playerAnimator.Play("skin_0_walk_outfit_1");
            }
            if (StaticValues.skin == 1)
            {
                playerAnimator.Play("skin_1_walk_outfit_1");
            }
            if (StaticValues.skin == 2)
            {
                playerAnimator.Play("skin_2_walk_outfit_1");
            }
            if (StaticValues.skin == 3)
            {
                playerAnimator.Play("skin_3_walk_outfit_1");
            }
            if (StaticValues.skin == 4)
            {
                playerAnimator.Play("skin_4_walk_outfit_1");
            }
            if (StaticValues.skin == 5)
            {
                playerAnimator.Play("skin_5_walk_outfit_1");
            }
            if (StaticValues.skin == 6)
            {
                playerAnimator.Play("skin_6_walk_outfit_1");
            }
            if (StaticValues.skin == 7)
            {
                playerAnimator.Play("skin_7_walk_outfit_1");
            }

            if (StaticValues.skin == 8)
            {
                playerAnimator.Play("skin_0_walk_outfit_2");
            }
            if (StaticValues.skin == 9)
            {
                playerAnimator.Play("skin_1_walk_outfit_2");
            }
            if (StaticValues.skin == 10)
            {
                playerAnimator.Play("skin_2_walk_outfit_2");
            }
            if (StaticValues.skin == 11)
            {
                playerAnimator.Play("skin_3_walk_outfit_2");
            }
            if (StaticValues.skin == 12)
            {
                playerAnimator.Play("skin_4_walk_outfit_2");
            }
            if (StaticValues.skin == 13)
            {
                playerAnimator.Play("skin_5_walk_outfit_2");
            }
            if (StaticValues.skin == 14)
            {
                playerAnimator.Play("skin_6_walk_outfit_2");
            }
            if (StaticValues.skin == 15)
            {
                playerAnimator.Play("skin_7_walk_outfit_2");
            }

            if (StaticValues.skin == 16)
            {
                playerAnimator.Play("skin_0_walk_outfit_3");
            }
            if (StaticValues.skin == 17)
            {
                playerAnimator.Play("skin_1_walk_outfit_3");
            }
            if (StaticValues.skin == 18)
            {
                playerAnimator.Play("skin_2_walk_outfit_3");
            }
            if (StaticValues.skin == 19)
            {
                playerAnimator.Play("skin_3_walk_outfit_3");
            }
            if (StaticValues.skin == 20)
            {
                playerAnimator.Play("skin_4_walk_outfit_3");
            }
            if (StaticValues.skin == 21)
            {
                playerAnimator.Play("skin_5_walk_outfit_3");
            }
            if (StaticValues.skin == 22)
            {
                playerAnimator.Play("skin_6_walk_outfit_3");
            }
            if (StaticValues.skin == 23)
            {
                playerAnimator.Play("skin_7_walk_outfit_3");
            }
        }
        else
        {
            playerAnimator.enabled = false;
        }

        // Flip the sprite based on movement direction
        if (horizontalInput > 0 && !isFacingRight)  // Moving right but facing left
        {
            FlipCharacter();  // Flip to face right
        }
        else if (horizontalInput < 0 && isFacingRight)  // Moving left but facing right
        {
            FlipCharacter();  // Flip to face left
        }
        //GameObject spell = Instantiate(spellPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.5f), Quaternion.identity);
        //spell.transform.position += new Vector3(1, 0, 0);
        if (Input.GetKeyDown("space"))
        {
            //spell.transform.Translate(2, 0, 0);
            //float step = 5 * Time.deltaTime; // calculate distance to move
            //spell.transform.position = Vector3.MoveTowards(spell.transform.position, (100,50,0), step);


            //Vector3 direction = (target.position - transform.position).normalized;
            //transform.position += direction * speed * Time.deltaTime;
            
            spell = Instantiate(spellPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), Quaternion.identity);
            active = true;

            

        }

        if (Input.GetKeyDown(KeyCode.Q) && oil>0)
        {
            //spell.transform.Translate(2, 0, 0);
            //float step = 5 * Time.deltaTime; // calculate distance to move
            //spell.transform.position = Vector3.MoveTowards(spell.transform.position, (100,50,0), step);


            //Vector3 direction = (target.position - transform.position).normalized;
            //transform.position += direction * speed * Time.deltaTime;

            oilBottle = Instantiate(oilBottlePrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), Quaternion.identity);
            

            oil--;
            oilBottle.tag = "Finish";
            active=true;

        }


        if (active)
        {
            if(spell)
            {
                test.transform.position = new Vector2(this.transform.position.x + 100f, this.transform.position.y);
                target = test.transform;
                step = 0.5f * Time.deltaTime; // calculate distance to move
                spell.transform.position = Vector2.MoveTowards(spell.transform.position, target.position, step);
            }

            if (oilBottle)
            {
                test.transform.position = new Vector2(this.transform.position.x + 100f, this.transform.position.y);
                target = test.transform;
                step = 2f * Time.deltaTime; // calculate distance to move
                oilBottle.transform.position = Vector2.MoveTowards(oilBottle.transform.position, target.position, step);
            }
        }


    }

    void FixedUpdate()
    {
        // Move the character using physics (in FixedUpdate for consistent physics)
        // We keep the current vertical velocity (y) and only change horizontal (x)

        //rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        //rb.velocity = new Vector2(verticalInput * moveSpeed, rb.velocity.x);
        
        
    }

    // Flip the character's facing direction
    void FlipCharacter()
    {
        isFacingRight = !isFacingRight;  // Toggle facing direction flag
        spriteRenderer.flipX = !spriteRenderer.flipX;  // Flip the sprite visually
        hair.GetComponent<SpriteRenderer>().flipX = !hair.GetComponent<SpriteRenderer>().flipX;  // Flip the sprite visually
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            //if((enemies_eliminated == 2 && currentSceneName == "Level 1") || (enemies_eliminated == 3 && currentSceneName == "Level 2") || (enemies_eliminated == 4 && currentSceneName == "Level 3"))
            //{
                StaticValues.roomFinished = 1;
                roomRefreshed = 1;
            //}


        }

        if (collision.gameObject.CompareTag("Chest"))
        {
            int i = UnityEngine.Random.Range(1, 4);
            if (i == 1)
            {
                canvas.SetActive(true);
                chest1_screen.SetActive(true);
            }
            if (i == 2)
            {
                canvas.SetActive(true);
                chest2_screen.SetActive(true);
            }
            if (i == 3)
            {
                canvas.SetActive(true);
                chest3_screen.SetActive(true);
            }
            if (i==4)
            {
                canvas.SetActive(true);
                chest4_screen.SetActive(true);
            }

            Destroy(collision.gameObject);

        }


    }
}
