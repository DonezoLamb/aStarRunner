using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public float rotationSpeed;
    GameObject thisGameObject;
    // Start is called before the first frame update
    void Start()
    {
        thisGameObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        thisGameObject.transform.Rotate(new Vector3(0,0,rotationSpeed*Time.deltaTime));
    }//spins the enemy that is attached, for aesthetic purposes only
}
