using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour

{
    public Camera cam;
    public LineRenderer line;
    public Transform firepoint;
    private Quaternion p_rotation;

    void Start()
    {
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            EnableLaser();

        }
        if(Input.GetButton("Fire2"))
        {
            UpdateLaser();
        }
        if(Input.GetButtonUp("Fire2"))
        {
            DisableLaser();
        }

        RotateToMouse();
   
    }

    void DisableLaser()
    {
        line.enabled = false;
    }

     void UpdateLaser()
    {
        var mousepos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        line.SetPosition(0, firepoint.position);
        line.SetPosition(1, mousepos);
    }

    void EnableLaser()
    {
        line.enabled = true;
    }

    void RotateToMouse()
    {
        Vector2 direction= cam.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        p_rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = p_rotation;
    }
}
