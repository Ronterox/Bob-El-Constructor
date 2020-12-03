using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    [SerializeField] private Controller playerController;

    [Header("Animations and Sounds")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Animator p_animator;

    private void Awake()
    {
        p_animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (playerController == null) return;

        p_animator.SetBool("moving", playerController.directionInput != 0);
        if (playerController.directionInput != 0) spriteRenderer.flipX = playerController.directionInput > 0 ? false : true;
    }
}
