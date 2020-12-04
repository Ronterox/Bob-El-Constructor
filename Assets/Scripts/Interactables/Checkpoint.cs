using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class onCollision : UnityEvent { }

public class Checkpoint : MonoBehaviour {

    [SerializeField] private onCollision enterCheckpoint;
    public static Vector3 reachedPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (transform.position.x > reachedPoint.x)
            {
                reachedPoint = transform.position;
                enterCheckpoint.Invoke();
            }
         
        }
    }
}
