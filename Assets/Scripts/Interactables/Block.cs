using Managers;
using UnityEngine;

namespace Interactables
{
    [System.Serializable]
    public struct PaintedBlock
    {
        [Tooltip("Material of new collider 2D")]
        public PhysicsMaterial2D material2D;

        [Tooltip("New color of the block")] public Color color;
        public BlockEffect blockEffect;
    }

    public enum BlockEffect { None, Bouncy, Slider }

    [RequireComponent(typeof(Rigidbody2D))]
    public class Block : Draggable
    {
        [Header("Hammer Options")]
        [SerializeField]
        private Vector3 rotation = new Vector3(0, 0, 90);

        private Rigidbody2D p_rigidbody;

        private SpriteRenderer[] p_spriteRenderers;

        [Header("Brush Options")]
        [SerializeField]
        private PaintedBlock[] paintedVariations;

        private int p_pointer;

        [Header("Painted Bouncy Options")]
        [SerializeField]
        private float bouncinessMultiplier = 3f;

        [Tooltip("The limit velocity for the player per bounce")]
        [SerializeField]
        private float velocityOnBounceLimit = 50f;

        [Header("Painted Slider Options")]
        [SerializeField]
        private float sliderMultiplier = 3f;

        [Tooltip("The limit velocity for the player while sliding")]
        [SerializeField]
        private float velocityOnSlideLimit = 50f;

        protected override void Awake()
        {
            base.Awake();
            p_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            p_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!pr_isBeingDrag) return;
            if (Input.GetKeyDown(KeyCode.Space)) CheckToolApplyAction();
        }

        /// <summary>
        /// Applies an action to the block depending of the tool the player has
        /// </summary>
        private void CheckToolApplyAction()
        {
            switch (GameManager.Instance.currentPlayerTool)
            {
                case PlayerTool.None: transform.Rotate(rotation); break;
                case PlayerTool.Hammer: gameObject.SetActive(false); break;
                case PlayerTool.Brush: PaintBlock(); break;
                case PlayerTool.Static: p_rigidbody.isKinematic = !p_rigidbody.isKinematic; break;
                default: throw new System.ArgumentOutOfRangeException();
            }

            p_rigidbody.velocity = Vector2.zero;
        }

        /// <summary>
        /// Sets the color to all sprite renderer's children
        /// </summary>
        /// <param name="color">the color to be set to</param>
        private void SetBlockColor(Color color) { foreach (SpriteRenderer sprite in p_spriteRenderers) sprite.color = color; }

        /// <summary>
        /// Paints the sprite renderer of the block and adds a material to its collider
        /// </summary>
        private void PaintBlock()
        {
            PaintedBlock thisPaintedBlock = paintedVariations[(p_pointer = (p_pointer + 1) % paintedVariations.Length)];
            SetBlockColor(thisPaintedBlock.color);
            pr_collider.sharedMaterial = thisPaintedBlock.material2D;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            Vector2 velocity = collision.collider.attachedRigidbody.velocity;
            switch (paintedVariations[p_pointer].blockEffect)
            {
                case BlockEffect.None: break;
                case BlockEffect.Bouncy: collision.collider.attachedRigidbody.velocity = new Vector2(0, velocity.y * bouncinessMultiplier % velocityOnBounceLimit); break;
                case BlockEffect.Slider: collision.collider.attachedRigidbody.velocity = new Vector2(velocity.x * sliderMultiplier % velocityOnSlideLimit, 0); break;
                default: throw new System.ArgumentOutOfRangeException();
            }
        }
    }
}
