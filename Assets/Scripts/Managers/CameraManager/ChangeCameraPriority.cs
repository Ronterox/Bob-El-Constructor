using UnityEngine;

namespace Managers.CameraManager
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChangeCameraPriority : MonoBehaviour 
    {
        [SerializeField] private string cameraId;
        [Tooltip("If checked, once it exist the collider it will change back to the camera before hitting the collider")]
        [SerializeField] private bool onExitChangeBack;
        [SerializeField] private int priority = 20;
        
        private string p_oldCameraId;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            p_oldCameraId = CameraManager.Instance.currentCameraID;
            CameraManager.Instance.SetPriority(cameraId, priority);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (onExitChangeBack && collision.CompareTag("Player")) CameraManager.Instance.SetPriority(p_oldCameraId);
        }
    }
}
