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
    private Animator p_SpriteAnimator;

    private void Awake()
    {
        p_SpriteAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(p_CollisionCounter == 0)
        {
            p_SpriteAnimator.SetBool("IsPressed", true);
            onButtonEventEnter.Invoke();
        }
        p_CollisionCounter++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p_CollisionCounter--;
        if (p_CollisionCounter == 0)
        {
            p_SpriteAnimator.SetBool("IsPressed", false);
            onButtonEventExit.Invoke(); 
        }
    }
}
