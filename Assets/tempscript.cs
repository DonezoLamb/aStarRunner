using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class tempscript : MonoBehaviour
{
    public GameObject hostObj;
    public AstarPath testPath;
    public float xMove;
    public float yMove;
    // Start is called before the first frame update
    void Start()
    {
        testPath = hostObj.GetComponent<AstarPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //does not move the grid

            Debug.Log(testPath.data.gridGraph.center);
            testPath.data.gridGraph.center.x += xMove;
            //works to redraw grid in current location
            testPath.Scan();
        }
    }
}
