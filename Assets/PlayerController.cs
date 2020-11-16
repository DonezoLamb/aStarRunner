using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerBody;

    //checks for the ground and is used in player jumping checks
    public Transform groundCheck;
    float groundRadius =0.1f;
    public LayerMask isGround;

    //specific to movement speed calculations
    public float movespeed;
    public float minSpeed = 0;
    public float defaultSpeed;
    public float maxspeed = 0;
    char prevButton;

    //affects time to reach desired speed
    public float accel = 0;
    float msChangeTime;
    float msChangeTimeReset=1;

    //variables specific to jump controls
    public float jumpforce = 0;
    public float jumpTime;
    float jumpTimeReset;

    //here to prevent bug when closing the menu and jumps....
    //TODO: add more code/ or a new approach
    public bool gamePaused = false;//temp?
    public bool uiDisableJump = false;


    void Start()
    {
        movespeed = defaultSpeed;
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        jumpTimeReset = jumpTime;
    }//sets variable resets and gets the players RB

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            //modifies running speed
            //change to take less lines and ignore redundant calls            
            if (Input.GetKey(KeyCode.A) && minSpeed <= movespeed)
            {
                ModifyMovespeed(movespeed, minSpeed, 'a');
            }
            else if (Input.GetKey(KeyCode.D) && maxspeed >= movespeed)
            {
                ModifyMovespeed(movespeed, maxspeed, 'd');
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.A))
            {
                ModifyMovespeed(movespeed, defaultSpeed, 'n');
            }

            //jump code, to my knowledge works as intended ATM
            if (Input.GetKeyDown(KeyCode.Space) && OnRunnable() && !uiDisableJump)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpforce);
            }
            else if (Input.GetKey(KeyCode.Space) && jumpTime > 0f && !OnRunnable() && !uiDisableJump)
            {
                jumpTime -= Time.deltaTime;
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpforce);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpTime = 0;
            }
            else if(!Input.GetKey(KeyCode.Space))
            {
                uiDisableJump = false;
            }//prevents pause jump bugs
        }
    }

    void FixedUpdate()
    {
        playerBody.velocity = new Vector2(movespeed, playerBody.velocity.y);
    }//moves the player, in fixedUpdate speed is consistent

    
    bool OnRunnable()
    {        
        Collider2D grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isGround);
        if (grounded != null)
        {
            jumpTime = jumpTimeReset;
            return true;
        }
        else
        {
            return false;
        }
    }//checks to see if players feet is on the ground, enables jumping, called when player tries to jump

    void ModifyMovespeed(float currentMS, float msGoal, char curButton)
    {
        if(curButton!=prevButton)
        {
            msChangeTime = msChangeTimeReset;
        }

        movespeed = Mathf.Lerp(currentMS, msGoal, msChangeTime);
        msChangeTime -= Time.deltaTime* accel;


        prevButton = curButton;
    }//input (a or d), changes player speed, gets comands from Update
}
