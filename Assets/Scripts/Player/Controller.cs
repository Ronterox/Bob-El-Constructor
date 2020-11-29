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
    private float pf_timer;
    private float pf_pressedJumpTimer;

    private void Awake()
    {
        p_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        pb_isGrounded = Physics2D.OverlapCircle(groundPoint.position, feetRadius, ground);

        // We assign the rigidbody speed
        pf_inputHorizontalVelocity = p_rb.velocity.x;
        // We increment it in its direction
        pf_inputHorizontalVelocity += Input.GetAxisRaw("Horizontal");
        // Add some damping to reduce the gaining of speed
        pf_inputHorizontalVelocity *= Mathf.Pow(1f - pf_dampingHorizontal, Time.deltaTime * 10f);

        p_rb.velocity = new Vector2(pf_inputHorizontalVelocity, p_rb.velocity.y);
    }

    private void Update()
    {
        if (pf_timer > 0) pf_timer -= Time.deltaTime;

        if (pb_isGrounded) pf_timer = wasGroundedTime;

        if (pf_pressedJumpTimer > 0) pf_pressedJumpTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && pf_timer >= 0) pf_pressedJumpTimer = pressedJumpTime;

        if (pf_pressedJumpTimer > 0 && pf_timer > 0) p_rb.velocity = new Vector2(p_rb.velocity.x, jumpForce);

        if (Input.GetButtonUp("Jump"))
        {
            p_rb.velocity = new Vector2(p_rb.velocity.x, p_rb.velocity.y * jumpDamping);
        }
    }

    private void OnDrawGizmos()
    {
        if (pb_isGrounded) Gizmos.color = Color.green;
        else Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundPoint.position, feetRadius);
    }

}
