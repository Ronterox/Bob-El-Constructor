using Plugins.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class Player : PersistentSingleton<Player>
    {
        [SerializeField] private Controller playerController;

        [Header("Animations")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Animator p_animator;

        [Header("Sounds")]
        [SerializeField] private float footstepSoundDelay;
        private float p_footstepTime;
        private readonly int p_moving = Animator.StringToHash("Moving");
        private readonly int p_onAir = Animator.StringToHash("onAir");

        protected override void Awake()
        {
            base.Awake();
            p_animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            p_animator.SetBool(p_moving, playerController.directionInput != 0);
            if (playerController.directionInput != 0) spriteRenderer.flipX = !(playerController.directionInput > 0);

            p_animator.SetBool(p_onAir, !playerController.groundDetector.isGrounded);

            if (playerController.directionInput == 0 || !playerController.groundDetector.isGrounded || !(Time.time - p_footstepTime >= footstepSoundDelay)) return;
            p_footstepTime = Time.time;
            SoundManager.Instance.Play("Footstep", true);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!playerController.groundDetector.isGrounded) SoundManager.Instance.Play("Player Hit", true);
        }
    }
}
