using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct CMCamera
{
    public string id;
    public CinemachineVirtualCamera virtualCamera;
}

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CMCamera[] cinemachineVirtualCamera;
    public void SetPriority(string id)
    {
        foreach (CMCamera camera in cinemachineVirtualCamera)
        {
            if (camera.id.Equals(id)) camera.virtualCamera.Priority = 20;
            else camera.virtualCamera.Priority = 0;
        }
    }
}
