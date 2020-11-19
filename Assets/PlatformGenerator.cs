using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeTilSpawn;
    float spawnTimeReset;
    public GameObject basePlatform;
    float xOffsetMin = 13;
    float xOffsetMax = 17;
    float curX = 10;

    float curYOffset;
    float yOffsetDist = 2;
    float yOffsetMax = 5;
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
        if(Input.GetKeyDown(KeyCode.F))
        {
            SetNewSpawnPoint();
        }
    }

    public void CallSpawner()
    {
        SpawnPlatform();
    }

    public void SpawnPlatform()
    {
        timeTilSpawn = spawnTimeReset;
        SetNewSpawnPoint();
        GameObject newPlat = Instantiate(basePlatform, spawnPoint, Quaternion.identity);
        newPlat.transform.localScale += new Vector3(1f,10f,0);
    }


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

        if(curYOffset>yOffsetMax)
        {
            curYOffset = yOffsetMax;
        }
        else if(curYOffset < -yOffsetMax)
        {
            curYOffset = -yOffsetMax;
        }
        spawnPoint = new Vector3(curX, curYOffset, 0);
    }
}
