using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [Range(0, 1f)] [SerializeField] private float jumpDamping;
    [Range(0, 101f)] [SerializeField] private float pf_dampingHorizontal;

    private float pf_inputHorizontalVelocity;
    private Rigidbody2D p_rb;

    private bool pb_isGrounded;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float feetRadius;

    [SerializeField] private float pressedJumpTime = 0.3f;
    [SerializeField] private float wasGroundedTime = 0.3f;
    private float pf_groundedTimer;
    private float pf_pressedJumpTimer;

    [SerializeField]private SpriteRenderer p_spriterender;
    private Animator p_animator;

    private void Awake()
    {
        p_rb = gameObject.GetComponent<Rigidbody2D>();
        p_animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float directionInput= Input.GetAxisRaw("Horizontal");
        pb_isGrounded = Physics2D.OverlapCircle(groundPoint.position, feetRadius, ground);

        // We assign the rigidbody speed
        pf_inputHorizontalVelocity = p_rb.velocity.x;
        // We increment it in its direction
        pf_inputHorizontalVelocity += directionInput;
        if (directionInput > 0)
        {
            p_spriterender.flipX = false;
            p_animator.SetBool("moving", true);

        }else if (directionInput < 0)
        {
            p_spriterender.flipX = true;
            p_animator.SetBool("moving", true);
        }
        else if (directionInput == 0) 
        {
            p_animator.SetBool("moving", false);

        }
        // Add some damping to reduce the gaining of speed
        pf_inputHorizontalVelocity *= Mathf.Pow(1f - pf_dampingHorizontal, Time.deltaTime * 10f);

        p_rb.velocity = new Vector2(pf_inputHorizontalVelocity, p_rb.velocity.y);

        if (pf_pressedJumpTimer > 0 && pf_groundedTimer > 0)
        {
            p_rb.velocity = new Vector2(p_rb.velocity.x, jumpForce);
            pf_pressedJumpTimer = 0f; pf_groundedTimer = 0f;
        }

        if (Input.GetButtonUp("Jump")) p_rb.velocity = new Vector2(p_rb.velocity.x, p_rb.velocity.y * jumpDamping);
    }

    private void Update()
    {
        if (pf_groundedTimer > 0) pf_groundedTimer -= Time.deltaTime;

        if (pb_isGrounded) pf_groundedTimer = wasGroundedTime;

        if (pf_pressedJumpTimer > 0) pf_pressedJumpTimer -= Time.deltaTime;
        
        if (Input.GetButtonDown("Jump") && pf_groundedTimer > 0) pf_pressedJumpTimer = pressedJumpTime;
    }

    private void OnDrawGizmos()
    {
        if (pb_isGrounded) Gizmos.color = Color.green;
        else Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundPoint.position, feetRadius);
    }

}
