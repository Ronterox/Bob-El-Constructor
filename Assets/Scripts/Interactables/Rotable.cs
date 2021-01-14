using UnityEngine;

namespace Interactables
{
    public class Rotable : MonoBehaviour 
    {
        [SerializeField] private Vector3 rotation;

        /// <summary>
        /// Rotates the object to the vector3 Rotation
        /// </summary>
        public void Rotate() => transform.Rotate(rotation);
    }
}
