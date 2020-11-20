using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //TODO see if this affects gothub
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public AstarPath thisGrid;

    public float closeToEdge;
    float centerOfGrid;
    float toSideOfGrid;
    // Start is called before the first frame update
    void Start()
    {
        /*seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);*/
        centerOfGrid = thisGrid.data.gridGraph.center.x;
        toSideOfGrid = (thisGrid.data.gridGraph.width / 2) * thisGrid.data.gridGraph.nodeSize;
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (path == null)
        {
            return;
        }
        if(currentWaypoint>=path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance<nextWaypointDistance)
        {
            currentWaypoint++;
        }*/
        BoundsCheck();
    }



    void BoundsCheck()
    {
        //Debug.Log("grid center: " + thisGrid.data.gridGraph.center.x);
        //Debug.Log("grid width: " + thisGrid.data.gridGraph.width);
        //Debug.Log("Enemy Loc: " + transform.position.x);
        if (centerOfGrid+toSideOfGrid < transform.position.x + closeToEdge)
        {
            Debug.Log("activted");
            //keeps grid lined up with platforms on X axis
            centerOfGrid = Mathf.Round((transform.position.x + toSideOfGrid) * 2f) *.5f +.25f;
            thisGrid.data.gridGraph.center.x = centerOfGrid;
            //keeps grid lined up with the platform Y axis
            thisGrid.data.gridGraph.center.y = Mathf.Round(transform.position.y *2f) *.5f;
            thisGrid.Scan();
        }
    }
}
