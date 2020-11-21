using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }//erases objects that leave the players view
}
