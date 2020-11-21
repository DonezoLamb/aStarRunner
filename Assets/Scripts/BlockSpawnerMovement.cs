using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerMovement : MonoBehaviour//TODO: rework this whole script, code should be implemented better
{
    public float moveSpeed;
    public Camera cam;
    public SpriteRenderer topRight;
    public SpriteRenderer topLeft;
    public SpriteRenderer botRight;
    public SpriteRenderer botLeft;

    bool horzPressed = false;
    bool vertPressed = false;

    bool down;
    bool right;
    bool callWallColor=false;
    Vector3 viewPos;
    Color defaultColor = new Color(0, 1, 1, 1);
    Color wallColor = new Color(1, 0, 0, 1);
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        viewPos = cam.WorldToViewportPoint(transform.position);
        //these 4 statements are only for the cursors movement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            horzPressed = true;
            if(viewPos.x < .4f)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                callWallColor = true;
            }
        }//all of these if and else statements limit spawner movement and let it know when at bounds
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            right = false;
            horzPressed = true;
            if (viewPos.x > .1f)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else
            {
                callWallColor = true;
            }
        }
        else
        {
            right = false;
            horzPressed = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            down = false;
            vertPressed = true;
            if(viewPos.y < .9f)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                callWallColor = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            down = true;
            vertPressed = true;
            if (viewPos.y > .1f)
            {
                transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            }
            else
            {
                callWallColor = true;
            }
        }
        else
        {
            down = false;
            vertPressed = false;
        }
        NormalizeColor();//color reset is here to prevent bugs when at bounds
    }

    void SetWallColor()
    {
        if (viewPos.y <= .1f)
        {
            botLeft.color = wallColor;
            botRight.color = wallColor;
        }
        if (viewPos.y >= .9f)
        {
            topLeft.color = wallColor;
            topRight.color = wallColor;
        }
        if (viewPos.x <= .1f)
        {
            topLeft.color = wallColor;
            botLeft.color = wallColor;
        }
        if (viewPos.x >= .4f)
        {
            topRight.color = wallColor;
            botRight.color = wallColor;
        }
        callWallColor = false;
    }//not proud of this one, changes wall color, should probably be worked into body when reviewing scripts

    void NormalizeColor()
    {
        if(horzPressed)
        {
            if(right)
            {
                topLeft.color = defaultColor;
                botLeft.color = defaultColor;
            }
            else
            {
                topRight.color = defaultColor;
                botRight.color = defaultColor;
            }
        }
        if(vertPressed)
        {
            if (down)
            {
                topLeft.color = defaultColor;
                topRight.color = defaultColor;
            }
            else
            {
                botLeft.color = defaultColor;
                botRight.color = defaultColor;
            }
        }
        else if (!horzPressed &&!vertPressed)
        {
            topLeft.color = defaultColor;
            topRight.color = defaultColor;
            botLeft.color = defaultColor;
            botRight.color = defaultColor;
        }
        if(callWallColor)
        {
            SetWallColor();
        }
    }//TODO: update to remove unnessecary calls anc checks
}
