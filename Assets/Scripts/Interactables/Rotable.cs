using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotable : MonoBehaviour 
{
    [SerializeField] Vector3 rotation;

    /// <summary>
    /// Rotates the object to the vector3 Rotation
    /// </summary>
    public void Rotate()
    {
        transform.Rotate(rotation);
    }
}
