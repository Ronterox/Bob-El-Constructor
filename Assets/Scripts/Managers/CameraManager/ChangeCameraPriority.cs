using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPriority : MonoBehaviour 
{
    [SerializeField] private string cameraId;
    [Tooltip("If checked, once it exist the collider it will change back to the camera before hitting the collider")]
    [SerializeField] private bool onExitChangeBack;
    private string oldCameraId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            oldCameraId = CameraManager.instance.currentCameraID;
            CameraManager.instance.SetPriority(cameraId);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onExitChangeBack && collision.CompareTag("Player"))
        {
            CameraManager.instance.SetPriority(oldCameraId);
        }
    }
}
