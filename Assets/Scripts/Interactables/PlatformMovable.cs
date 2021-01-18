using UnityEngine;

namespace Interactables
{
    public class PlatformMovable : Movable
    {
        private void OnCollisionEnter2D(Collision2D other) => other.transform.parent = transform;

        private void OnCollisionExit2D(Collision2D other) => other.transform.parent = null;
    }   
}
