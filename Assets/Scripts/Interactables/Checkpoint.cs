using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class onCollision : UnityEvent { }

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private onCollision enterCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.checkpoint = transform;
            gameObject.SetActive(false);
            enterCheckpoint.Invoke();
        }
    }
}
