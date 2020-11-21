using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //players location
    public Transform target;

    //updated node and speed
    float speed = 700f;
    float nextWaypointDistance = 1f;
    float newPathTime = .25f;

    //where is the enemy
    Path path;
    int currentWaypoint = 0;

    //probably remove, not truesly being used
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    //used in checking how close the the edge of the grid enemy is
    public AstarPath thisGrid;
    float closeToEdge = 3;
    float centerOfGrid;
    float toSideOfGrid;

    //for adding extra speed on the X axis
    float additionalXForce = 150;
    Vector2 addXForceDir = new Vector2(2,0);


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //update players location
        InvokeRepeating("UpdatePath", 0f, newPathTime);

        //math for moving grid in
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
        if (path == null)
        {
            return;
        }

        //currently no real function, probably remove
        if(currentWaypoint>=path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        //moves enemy according to A*
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        
        if(transform.position.x<target.transform.position.x)
        {
            force = additionalXForce * addXForceDir * Time.deltaTime;
            rb.AddForce(force);
        }//if behind the player move more on the X axis, before the addition of this the enemy follow too strictly on the Y. 

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance<nextWaypointDistance)
        {
            currentWaypoint++;
        }
        BoundsCheck();
    }



    void BoundsCheck()
    {
        
        if (centerOfGrid+toSideOfGrid < transform.position.x + closeToEdge)
        {
            //keeps grid lined up with platforms on X axis
            centerOfGrid = Mathf.Round((transform.position.x + toSideOfGrid) * 2f) *.5f +.25f;
            thisGrid.data.gridGraph.center.x = centerOfGrid;
            //keeps grid lined up with the platform Y axis
            thisGrid.data.gridGraph.center.y = Mathf.Round((transform.position.y+target.position.y)) *.5f;
            thisGrid.Scan();
        }
    }//moves A* grid to place enemy at far left and center(Y) between player and enemy
}
