using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerObsSpawner : MonoBehaviour
{
    public bool gamePaused;
    public int curCharges = 0;
    public int maxCharges = 2;
    public int index;

    public float curTime;
    public float ChargeTime;
    public GameObject[] placeableBlock;
    GameObject tempPlacement;

    public Image barFill;
    public Image nextBlock;
    public TextMeshProUGUI chargeCounters;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleOrder();
        //TODO make it an image
        nextBlock.sprite = placeableBlock[index].GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gamePaused)
        {
            //using charges and spawn block  
            if (Input.GetKeyDown(KeyCode.LeftShift) && curCharges>0)
            {
                Debug.Log("place block");
                
                curCharges--;
                chargeCounters.text = curCharges.ToString();
                Instantiate(placeableBlock[index], transform.position, Quaternion.identity);
                FindObjectOfType<AstarPath>().Scan();
                index++;
                if (index>=placeableBlock.Length)
                {
                    index = 0;
                    ShuffleOrder();
                }
                nextBlock.sprite = placeableBlock[index].GetComponent<SpriteRenderer>().sprite;

            }
            if (curTime<ChargeTime && curCharges<maxCharges)
            {
                //Debug.Log("time go up");
                barFill.fillAmount = curTime / ChargeTime;
                curTime += Time.deltaTime;
            }
            else if (curTime >= ChargeTime && curCharges < maxCharges)
            {
                //Debug.Log("charges go up");

                curTime = 0;
                curCharges++;
                chargeCounters.text = curCharges.ToString();
            }
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
    }
}
