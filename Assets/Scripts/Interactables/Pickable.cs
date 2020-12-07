using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPickableEvent : UnityEvent<int> {}
[RequireComponent(typeof(BoxCollider2D))]
public class Pickable : MonoBehaviour 
{
    [Tooltip("If you select this it will be destroyed instead of disabled")]
    [SerializeField] private bool destroyOnPick;
    [SerializeField] private int valueOnPick = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (destroyOnPick) Destroy(gameObject);
            else gameObject.SetActive(false);
            GameManager.instance.onPickableEvent.Invoke(valueOnPick);
        }
    }
}
