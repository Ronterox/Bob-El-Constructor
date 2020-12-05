using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{
    [Header("Jump Config")]
    [SerializeField] private float jumpForce;
    [Range(0, 1f)] [SerializeField] private float jumpDamping;

    [Header("Horizontal Movement Config")]
    [Range(0, 1f)] [SerializeField] private float dampingHorizontal;

    private float pf_inputHorizontalVelocity;
    [HideInInspector] public float directionInput;
    private Rigidbody2D p_rb;

    [SerializeField] private float pressedJumpTime = 0.3f;
    private float pf_pressedJumpTimer;
    private bool pb_pressedJump;

    private GroundDetector p_groundDetector;

    private void Awake()
    {
        p_rb = GetComponent<Rigidbody2D>();
        p_groundDetector = GetComponent<GroundDetector>();
    }

    private void FixedUpdate()
    {
        directionInput = Input.GetAxisRaw("Horizontal");

        // We assign the rigidbody speed
        pf_inputHorizontalVelocity = p_rb.velocity.x;
        // We increment it in its direction
        pf_inputHorizontalVelocity += directionInput;

        // Add some damping to reduce the gaining of speed
        pf_inputHorizontalVelocity *= Mathf.Pow(1f - dampingHorizontal, Time.deltaTime * 10f);

        p_rb.velocity = new Vector2(pf_inputHorizontalVelocity, p_rb.velocity.y);

        if (p_groundDetector.isGrounded) pb_pressedJump = false;

        if (pf_pressedJumpTimer > 0 && p_groundDetector.groundedTimer > 0 && !pb_pressedJump)
        {
            p_rb.velocity = new Vector2(p_rb.velocity.x, jumpForce);
            pf_pressedJumpTimer = 0f; p_groundDetector.groundedTimer = 0f;
        }

        if (Input.GetButtonUp("Jump")) p_rb.velocity = new Vector2(p_rb.velocity.x, p_rb.velocity.y * jumpDamping);
    }

    private void Update()
    {
        if (pf_pressedJumpTimer > 0) pf_pressedJumpTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && p_groundDetector.groundedTimer > 0)
        {
            pf_pressedJumpTimer = pressedJumpTime;
            pb_pressedJump = true;
        }
    }
}
