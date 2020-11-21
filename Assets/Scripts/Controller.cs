using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour {
    [SerializeField]private float speed;
    [SerializeField]private float jumpForce;

    private float inputDirection;
    private Rigidbody2D rb;

    private bool canJump;
    [SerializeField]private Transform groundPoint;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float feetRadius;


    private void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        canJump = Physics2D.OverlapCircle(groundPoint.position,feetRadius,ground);
        inputDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed*inputDirection,rb.velocity.y);


    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")&&canJump)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundPoint.position,feetRadius);
    }

}
