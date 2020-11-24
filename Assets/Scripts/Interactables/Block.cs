using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Block : Draggable
{
    [SerializeField]
    private Vector3 rotation = new Vector3(0,0,90);
    private Rigidbody2D p_rigidbody;
    private void Awake()
    {
        base.Awake();
        p_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (pb_isBeingDrag)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                transform.Rotate(rotation);
                p_rigidbody.velocity = Vector2.zero;
            }
        }
    }
}
