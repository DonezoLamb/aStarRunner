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


    public float accel = 0;
    float msChangeTime;
    float msChangeTimeReset=1;

    //variables specific to jump controls
    public float jumpforce = 0;
    public float jumpTime;
    float jumpTimeReset;

    //here to prevent bug when closing the menu and jumps....
    public bool pauseBuffer = false;
    void Start()
    {
        movespeed = defaultSpeed;
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        jumpTimeReset = jumpTime;
    }

    // Update is called once per frame
    void Update()
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
        else if(!Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.A)) 
        {
            ModifyMovespeed(movespeed, defaultSpeed,'n');
        }

        //jump code, to my knowledge works as intended ATM
        if (Input.GetKeyDown(KeyCode.Space) && OnRunnable() && !pauseBuffer)
        {
             playerBody.velocity = new Vector2(playerBody.velocity.x, jumpforce);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && OnRunnable() && pauseBuffer)
        {
            pauseBuffer = false;
        }
        else if (Input.GetKey(KeyCode.Space) && jumpTime > 0f && !OnRunnable())
        {
            jumpTime -= Time.deltaTime;
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpforce);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTime = 0;
        }
        //jump code end
    }
    void FixedUpdate()
    {
        playerBody.velocity = new Vector2(movespeed, playerBody.velocity.y);
    }

    //checks to see if players feet is on the ground, enables jumping
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
    }

    void ModifyMovespeed(float currentMS, float msGoal, char curButton)
    {
        if(curButton!=prevButton)
        {
            msChangeTime = msChangeTimeReset;
        }

        movespeed = Mathf.Lerp(currentMS, msGoal, msChangeTime);
        msChangeTime -= Time.deltaTime* accel;


        prevButton = curButton;
    }
}
