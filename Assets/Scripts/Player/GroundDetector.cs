using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour 
{
    [Header("Grounded Config")]
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float feetRadius;

    [SerializeField] private float wasGroundedTime = 0.3f;

    [HideInInspector]
    public float groundedTimer;

    [HideInInspector]
    public bool isGrounded;

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, feetRadius, ground);
        if (isGrounded) groundedTimer = wasGroundedTime;
    }

    private void Update()
    {
        if (groundedTimer > 0) groundedTimer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if (isGrounded) Gizmos.color = Color.green;
        else Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundPoint.position, feetRadius);
    }
}
