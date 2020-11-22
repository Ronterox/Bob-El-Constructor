using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour {
    [SerializeField]private float speed;
    [SerializeField]private float jumpForce;


    private float inputDirection;
    private Rigidbody2D rb;

    private bool isGrounded;
    [SerializeField]private Transform groundPoint;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float feetRadius;
    private float pf_timer;
    private float pf_pressedJumpTimer;
   


    private void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position,feetRadius,ground);
        inputDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed*inputDirection,rb.velocity.y);


    }

    private void Update()
    {
        if (pf_timer > 0)
        {
            pf_timer -= Time.deltaTime;
        }

        if (isGrounded)
        {
            pf_timer = 0.3f;
        }

        if (pf_pressedJumpTimer > 0)
        {
            pf_pressedJumpTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")&&pf_timer>=0)
        {
            pf_pressedJumpTimer = 0.3f;  
        }

        if (pf_pressedJumpTimer > 0 && pf_timer > 0)
        {
            rb.velocity = Vector2.up*jumpForce;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundPoint.position,feetRadius);
    }

}
