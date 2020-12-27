using Plugins.Tools;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour
    {
        [Tooltip("Select whether you want the object to collide while being drag")]
        [SerializeField] private bool isTriggerWhileDrag;
        [SerializeField] private bool blockRotationWhileDrag;

        private Camera p_camera;
        protected Collider2D pr_collider;

        protected bool pr_isBeingDrag;

        private Transform p_transform;

        protected virtual void Awake()
        {
            p_transform = transform;
            p_camera = Camera.main;
            pr_collider = GetComponent<Collider2D>();
        }

        /// <summary>
        /// To initialize the called of being drag, and add the GameObject alterations
        /// </summary>
        private void OnMouseDown()
        {
            SoundManager.Instance.Play("Block Pick");
            pr_isBeingDrag = true;
            if (isTriggerWhileDrag && !pr_collider.isTrigger) pr_collider.isTrigger = true;
            if (blockRotationWhileDrag) pr_collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        /// <summary>
        /// Can drag the GameObject in mouse range
        /// </summary>
        private void OnMouseDrag()
        {
            Vector2 mousePos = p_camera.ScreenToWorldPoint(Input.mousePosition);
            p_transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

        /// <summary>
        /// Changes the object collision if it was set to trigger
        /// </summary>
        private void OnMouseUp()
        {
            SoundManager.Instance.Play("Block Place");
            pr_isBeingDrag = false;
            if (isTriggerWhileDrag && pr_collider.isTrigger) pr_collider.isTrigger = false;
            if (blockRotationWhileDrag) pr_collider.attachedRigidbody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
