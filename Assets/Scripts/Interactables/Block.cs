using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Block : Draggable
{
    [Header("Hammer Options")]
    [SerializeField]
    private Vector3 rotation = new Vector3(0, 0, 90);
    private Rigidbody2D p_rigidbody;

    private SpriteRenderer p_spriteRenderer;

    [Header("Brush Options")]
    [SerializeField]
    private PaintedBlock[] paintedVariations;
    private int pi_pointer = 0;

    [Space]
    [SerializeField]
    private float bouncinessMultiplier = 3f;
    [SerializeField][Tooltip("The limit velocity for the player")]
    private float velocityLimit = 50f;

    protected override void Awake()
    {
        base.Awake();
        p_spriteRenderer = GetComponent<SpriteRenderer>();
        p_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (pb_isBeingDrag) 
            if (Input.GetKeyDown(KeyCode.LeftControl)) CheckToolApplyAction();
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
    /// Paints the sprite renderer of the block and adds a material to its collider
    /// </summary>
    private void PaintBlock()
    {
        PaintedBlock thisPaintedBlock = paintedVariations[(pi_pointer = (pi_pointer + 1) % paintedVariations.Length)];
        p_spriteRenderer.color = thisPaintedBlock.color;
        p_collider.sharedMaterial = thisPaintedBlock.material2D;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (paintedVariations[pi_pointer].material2D.bounciness > 0)
            {
                Vector2 velocity = collision.collider.attachedRigidbody.velocity;
                collision.collider.attachedRigidbody.velocity = new Vector2(0, velocity.y * bouncinessMultiplier % velocityLimit);
            }
        }
    }

}

[System.Serializable]
public struct PaintedBlock
{
    [Tooltip("Material of new collider 2D")]
    public PhysicsMaterial2D material2D;
    [Tooltip("New color of the block")]
    public Color color;
}