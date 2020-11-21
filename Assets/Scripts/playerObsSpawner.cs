using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerObsSpawner : MonoBehaviour
{
    //checks if variables can get updated
    public bool gamePaused;

    //keeps track of if block can be placed and num of placeable blocks
    public int curCharges = 0;
    public int maxCharges = 2;
    public float curTime;
    public float ChargeTime;

    //used to randomize block order
    public GameObject[] placeableBlock;
    GameObject tempPlacement;
    public int index;

    //for UI
    public Image barFill;
    public Image nextBlock;
    public TextMeshProUGUI chargeCounters;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleOrder();
        nextBlock.sprite = placeableBlock[index].GetComponent<SpriteRenderer>().sprite;
    }//randomizes the order that blocks get placed

    // Update is called once per frame
    void Update()
    {
        if(!gamePaused)
        {
            //using charges and spawn block  
            if (Input.GetKeyDown(KeyCode.LeftShift) && curCharges>0)
            {                
                curCharges--;
                chargeCounters.text = curCharges.ToString();
                Instantiate(placeableBlock[index], transform.position, Quaternion.identity);
                FindObjectOfType<AstarPath>().Scan();
                index++;
                if (index>=placeableBlock.Length)
                {
                    index = 0;
                    ShuffleOrder();
                }//if array has been traversed reshuffle and reset index
                nextBlock.sprite = placeableBlock[index].GetComponent<SpriteRenderer>().sprite;
            }//places the block
            if (curTime<ChargeTime && curCharges<maxCharges)
            {
                barFill.fillAmount = curTime / ChargeTime;
                curTime += Time.deltaTime;
            }//timer for getting more blocks
            else if (curTime >= ChargeTime && curCharges < maxCharges)
            {
                curTime = 0;
                curCharges++;
                chargeCounters.text = curCharges.ToString();
            }//adjusts the number of placeable blocks
        }

    }

    public void ShuffleOrder()
    {
        for(int i = 0; i<placeableBlock.Length;i++)
        {
            int rando = Random.Range(0, placeableBlock.Length);
            tempPlacement = placeableBlock[rando];
            placeableBlock[rando] = placeableBlock[i];
            placeableBlock[i] = tempPlacement;
        }
    }//shuffles the prefab array
}
