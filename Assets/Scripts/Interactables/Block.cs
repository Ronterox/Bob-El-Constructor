using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
    private int pi_pointer;

    protected override void Awake()
    {
        base.Awake();
        p_spriteRenderer = GetComponent<SpriteRenderer>();
        p_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (pb_isBeingDrag)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                switch(GameManager.instance.currentPlayerTool)
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
                }
                p_rigidbody.velocity = Vector2.zero;
            }
        }
    }

    private void PaintBlock()
    {
        PaintedBlock thisPaintedBlock = paintedVariations[(++pi_pointer) % paintedVariations.Length];
        p_spriteRenderer.color = thisPaintedBlock.color;
        p_collider.sharedMaterial = thisPaintedBlock.material2D;
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