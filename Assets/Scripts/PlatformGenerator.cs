using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    // Decides how often platforms spawn, TODO: in future update change to be based off of how many platforms have left the screen
    float timeTilSpawn =1.5f;
    float spawnTimeReset;


    public GameObject basePlatform;

    //distance on X axis to place new platform
    float xOffsetMin = 8;
    float xOffsetMax = 11;
    float curX = 10;

    //range on Y new platform can be placed
    float curYOffset;
    float yOffsetDist = 2;
    float yOffsetMax = 5;

    //actual spawn point for each platform
    Vector3 spawnPoint;

    void Start()
    {
        spawnTimeReset = timeTilSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        //check if time to spawn platform
        if(timeTilSpawn>0)
        {
            timeTilSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        
        timeTilSpawn = spawnTimeReset;
        SetNewSpawnPoint();
        GameObject newPlat = Instantiate(basePlatform, spawnPoint, Quaternion.identity);


        //if i want to modify the size of the platform
        //newPlat.transform.localScale += new Vector3(1f,10f,0);
    }//checks where to spawn platform then creates it


    //X axis aligns at .25 and .75
    //Y axis aligns at .00 and .5
    public void SetNewSpawnPoint()
    {
        //set x within range
        curX += Random.Range(xOffsetMin,xOffsetMax);
        curX = Mathf.Round(curX * 2f) * .5f + .25f;

        //set platform y to be similar to previous plat
        //TODO a line to make it more liekly to return to the middle after reaching an edge case
        curYOffset += Random.Range(-yOffsetDist, yOffsetDist);
        curYOffset = Mathf.Round(curYOffset * 2f) * .5f;


        //makes sure platforms do not leave the appropriate range
        if(curYOffset>yOffsetMax)
        {
            curYOffset = yOffsetMax;
        }
        else if(curYOffset < -yOffsetMax)
        {
            curYOffset = -yOffsetMax;
        }


        //sets spawn point
        spawnPoint = new Vector3(curX, curYOffset, 0);
    }
}
