using Cinemachine;
using Plugins.Tools;
using UnityEngine;

namespace Managers.CameraManager
{
    public class FollowPlayerPersistent : MonoBehaviour, MMEventListener<LoadedEvent>
    {
        [SerializeField] private CinemachineVirtualCamera followCamera;

        private void Awake()
        {
            if (followCamera == null) followCamera = GetComponent<CinemachineVirtualCamera>();
            if (followCamera.Follow == null) followCamera.Follow = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable() => this.MMEventStartListening();

        private void OnDisable() => this.MMEventStopListening();

        public void OnMMEvent(LoadedEvent eventType)
        {
            if (followCamera.Follow == null) followCamera.Follow = GameObject.FindWithTag("Player")?.transform;
        }
    }
}
