using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PaintedBlock
{
    [Tooltip("Material of new collider 2D")]
    public PhysicsMaterial2D material2D;
    [Tooltip("New color of the block")]
    public Color color;
    public BlockEffect blockEffect;
}
public enum BlockEffect
{
    None,
    Bouncy,
    Slider
}

[RequireComponent(typeof(Rigidbody2D))]
public class Block : Draggable
{
    [Header("Hammer Options")]
    [SerializeField] private Vector3 rotation = new Vector3(0, 0, 90);
    private Rigidbody2D p_rigidbody;

    private SpriteRenderer[] p_spriteRenderer;

    [Header("Brush Options")]
    [SerializeField] private PaintedBlock[] paintedVariations;
    private int pi_pointer = 0;

    [Header("Painted Bouncy Options")]
    [SerializeField] private float bouncinessMultiplier = 3f;
    [Tooltip("The limit velocity for the player per bounce")]
    [SerializeField] private float velocityOnBounceLimit = 50f;

    [Header("Painted Slider Options")]
    [SerializeField] private float sliderMultiplier = 3f;
    [Tooltip("The limit velocity for the player while sliding")]
    [SerializeField] private float velocityOnSlideLimit = 50f;

    protected override void Awake()
    {
        base.Awake();
        p_spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        p_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (pb_isBeingDrag) 
            if (Input.GetKeyDown(KeyCode.Space)) CheckToolApplyAction();
    }

    /// <summary>
    /// Applies an action to the block depending of the tool the player has
    /// </summary>
    private void CheckToolApplyAction()
    {
        switch (GameManager.instance.currentPlayerTool)
        {
            case PlayerTool.None:
                transform.Rotate(rotation);
                break;
            case PlayerTool.Hammer:
                gameObject.SetActive(false);
                break;
            case PlayerTool.Brush:
                PaintBlock();
                break;
            case PlayerTool.Static:
                p_rigidbody.isKinematic = !p_rigidbody.isKinematic;
                break;
        }
        p_rigidbody.velocity = Vector2.zero;
    }

    /// <summary>
    /// Sets the color to all sprite renderer childs
    /// </summary>
    /// <param name="color">the color to be set to</param>
    private void SetBlockColor(Color color) { foreach (SpriteRenderer sprite in p_spriteRenderer) sprite.color = color; }

    /// <summary>
    /// Paints the sprite renderer of the block and adds a material to its collider
    /// </summary>
    private void PaintBlock()
    {
        PaintedBlock thisPaintedBlock = paintedVariations[(pi_pointer = (pi_pointer + 1) % paintedVariations.Length)];
        SetBlockColor(thisPaintedBlock.color);
        p_collider.sharedMaterial = thisPaintedBlock.material2D;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 velocity = collision.collider.attachedRigidbody.velocity;
            switch (paintedVariations[pi_pointer].blockEffect)
            {
                case BlockEffect.None: break;
                case BlockEffect.Bouncy:
                    collision.collider.attachedRigidbody.velocity = new Vector2(0, velocity.y * bouncinessMultiplier % velocityOnBounceLimit);
                    break;
                case BlockEffect.Slider:
                    collision.collider.attachedRigidbody.velocity = new Vector2(velocity.x * sliderMultiplier %velocityOnSlideLimit, 0);
                    break;
            }
        }
    }

}