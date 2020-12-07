using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    [Tooltip("Select whether you want the object to collide while being drag")]
    [SerializeField] private bool isTriggerWhileDrag = false;
    [SerializeField] private bool blockRotationWhileDrag = false;

    private Camera p_camera;
    protected Collider2D p_collider;

    protected bool pb_isBeingDrag = false;

    protected virtual void Awake()
    {
        p_camera = Camera.main;
        p_collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// To initialize the called of being drag, and add the gameobject alterations
    /// </summary>
    void OnMouseDown()
    {
        if (isTriggerWhileDrag && !p_collider.isTrigger) p_collider.isTrigger = true;
        pb_isBeingDrag = true;
        if (blockRotationWhileDrag) p_collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    /// <summary>
    /// Can drag the gameobject in mouse range
    /// </summary>
    void OnMouseDrag()
    {
        Vector2 mousePos = p_camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    /// <summary>
    /// Changes the object collision if it was set to trigger
    /// </summary>
    private void OnMouseUp()
    {
        if (isTriggerWhileDrag && p_collider.isTrigger) p_collider.isTrigger = false;
        pb_isBeingDrag = false;
        if (blockRotationWhileDrag) p_collider.attachedRigidbody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
    }
}
