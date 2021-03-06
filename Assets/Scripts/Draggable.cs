﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Select whether you want the object to collide while being drag")]
    private bool isTriggerWhileDrag = false;
    private Camera p_camera;
    private Collider2D p_collider;

    private void Awake()
    {
        p_camera = Camera.main;
        p_collider = GetComponent<Collider2D>();
    }

    void OnMouseDrag()
    {
        if (isTriggerWhileDrag && !p_collider.isTrigger) p_collider.isTrigger = true; 
        Vector2 mousePos = p_camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        if (isTriggerWhileDrag && p_collider.isTrigger) p_collider.isTrigger = false;
    }
}
