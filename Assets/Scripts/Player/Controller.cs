using Managers;
using Plugins.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(GroundDetector))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        [Header("Jump Config")]
        [SerializeField] private float jumpForce;
        [Range(0, 1f)] [SerializeField] private float jumpDamping;

        [SerializeField] private float jumpPressedTime = 3f;
        private float jumpTimer;

        [Header("Horizontal Movement Config")]
        [Range(0, 1f)] [SerializeField] private float dampingHorizontal;
        [SerializeField] private float acceleration;

        private float p_inputHorizontalVelocity;
        [HideInInspector] public float directionInput;
        private Rigidbody2D p_rigidbody;

        public GroundDetector groundDetector;

        private void Awake()
        {
            p_rigidbody = GetComponent<Rigidbody2D>();
            if (groundDetector == null) groundDetector = GetComponent<GroundDetector>();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.gameIsPaused) return;
            directionInput = Input.GetAxisRaw("Horizontal");

            Vector2 velocity = p_rigidbody.velocity;

            // We assign the rigidbody speed
            p_inputHorizontalVelocity = velocity.x;
            // We increment it in its direction
            p_inputHorizontalVelocity += directionInput * acceleration;

            // Add some damping to reduce the gaining of speed
            p_inputHorizontalVelocity *= Mathf.Pow(1f - dampingHorizontal, Time.deltaTime * 10f);

            velocity.x = p_inputHorizontalVelocity;

            if (groundDetector.groundedTimer > 0 && jumpTimer > 0)
            {
                velocity.y = jumpForce;
                groundDetector.groundedTimer = 0f;
                jumpTimer = 0;
                SoundManager.Instance.Play("Jump");
            }

            if (Input.GetButtonUp("Jump")) velocity.y *= jumpDamping;

            p_rigidbody.velocity = velocity;
        }

        private void Update()
        {
            if (GameManager.Instance.gameIsPaused) return;
            if (jumpTimer > 0) jumpTimer -= Time.deltaTime;
            if (Input.GetButtonDown("Jump")) jumpTimer = jumpPressedTime;
        }
    }
}
