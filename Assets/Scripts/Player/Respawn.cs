using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    private bool pb_isRestarting = false;
    [SerializeField]private int pi_fallDistance;


    private void Update()
    {
        if (transform.position.y <= pi_fallDistance)
        {
            if (pb_isRestarting == false)
            {
                respawnPlayer();
            }
        }
     
    }

  
     public void respawnPlayer()
    {
        pb_isRestarting = true;
        transform.position = Checkpoint.reachedPoint;
        pb_isRestarting = false;


    }
}
