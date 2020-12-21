using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    [SerializeField] private Controller playerController;

    [Header("Animations")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Animator p_animator;

    [Header("Sounds")]
    [SerializeField] private float footstepSoundDelay;
    private float pf_footstepTime = 0;

    private void Awake() { p_animator = GetComponent<Animator>(); }

    private void FixedUpdate()
    {
        if (playerController == null) return;

        p_animator.SetBool("moving", playerController.directionInput != 0);
        if (playerController.directionInput != 0) spriteRenderer.flipX = playerController.directionInput > 0 ? false : true;

        if (playerController.directionInput != 0 
            && playerController.p_groundDetector.isGrounded 
            && (Time.time - pf_footstepTime) >= footstepSoundDelay)
        {
            pf_footstepTime = Time.time;
            SoundManager.instance.Play("Footstep", true);
        }
    }
}
