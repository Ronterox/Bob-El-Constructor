using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnButtonEvent:UnityEvent {}

public class Button : MonoBehaviour
{
    [SerializeField] private OnButtonEvent onButtonEventEnter;
    [SerializeField] private OnButtonEvent onButtonEventExit;
    private int p_CollisionCounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        p_CollisionCounter++;
        onButtonEventEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p_CollisionCounter--;
        if (p_CollisionCounter == 0) { onButtonEventExit.Invoke(); }
    }
}
