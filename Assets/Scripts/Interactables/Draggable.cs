using Plugins.Properties;
using Plugins.Tools;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour
    {
        [Header("While Being Drag...")]
        [Tooltip("Select whether you want the object to be in another layer while being drag")]
        public bool changeLayer;
        
        [Tooltip("Select to which layer the object will be passed to while being drag")]
        [SerializeField] private SingleUnityLayer whileDragLayer;
        private LayerMask defaultLayer;

        [SerializeField] private bool freezeRotation;

        private Camera p_camera;
        protected Collider2D pr_collider;

        protected bool pr_isBeingDrag;

        private Transform p_transform;
        private Vector3 mousePos;

        protected virtual void Awake()
        {
            defaultLayer = gameObject.layer;
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
            if (changeLayer) gameObject.layer = whileDragLayer.LayerIndex;
            if (freezeRotation) pr_collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        /// <summary>
        /// Can drag the GameObject in mouse range
        /// </summary>
        private void OnMouseDrag()
        {
            mousePos = p_camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            p_transform.position = mousePos;
        }

        /// <summary>
        /// Changes the object collision if it was set to trigger
        /// </summary>
        private void OnMouseUp()
        {
            SoundManager.Instance.Play("Block Place");
            pr_isBeingDrag = false;
            if (changeLayer) gameObject.layer = defaultLayer.value; 
            if (freezeRotation) pr_collider.attachedRigidbody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
