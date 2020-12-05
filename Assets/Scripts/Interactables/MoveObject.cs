using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    [SerializeField] private Vector2 xy;
    [SerializeField] private float velocity;
    private bool p_isPressed;
    private Rigidbody2D p_rb;

    private void Awake()
    {
        p_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (p_isPressed)
        {
            if (gameObject.transform.position.x != xy.x && gameObject.transform.position.y != xy.y)
            {
                p_rb.MovePosition(xy);
            }
            else
            {
                p_isPressed = false;
            }
          
           
        }
    }

    public void Teleport()
    {
        gameObject.transform.position = xy;
    }

    public void Move()
    {
        p_isPressed = true; 
    }
}
