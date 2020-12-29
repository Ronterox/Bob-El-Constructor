using System.Collections;
using Cinemachine;
using Plugins.Tools;
using UnityEngine;

namespace Managers.CameraManager
{
    [System.Serializable]
    internal struct CmCamera
    {
        public string id;
        public CinemachineVirtualCamera virtualCamera;
    }

    public class CameraManager : Singleton<CameraManager>
    {

        [SerializeField] private CinemachineBrain cinemachineBrain;
        [Tooltip("The ID of the camera to be set as the manager awakes")]
        [SerializeField] private string awakeCamera;

        private Coroutine p_currentShakeCoroutine;
        private CinemachineBasicMultiChannelPerlin p_currentNoise;

        [SerializeField] private CmCamera[] cinemachineVirtualCameras;

        protected override void Awake()
        {
            base.Awake();
            SetPriority(awakeCamera);
        }

        public string CurrentCameraID
        {
            get 
            {
                foreach (CmCamera cam in cinemachineVirtualCameras) if (cam.virtualCamera.Priority >= 20) return cam.id;
                return "";
            }
        }

        /// <summary>
        /// Sets the priority of a camera to 'maximum'. So it changes to that camera
        /// </summary>
        /// <param name="id">Id of the camera to set priority to</param>
        public void SetPriority(string id)
        {
            foreach (CmCamera cam in cinemachineVirtualCameras) { cam.virtualCamera.Priority = cam.id.Equals(id) ? 20 : 0; }
        }

        /// <summary>
        /// Shakes the prioritize camera
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="frequency"></param>
        /// <param name="duration"></param>
        public void Shake(float amplitude, float frequency, float duration)
        {
            StopShake();

            p_currentShakeCoroutine = StartCoroutine(ShakeCo(amplitude, frequency, duration));
        }

        /// <summary>
        /// Stops Current Coroutine Shake
        /// </summary>
        public void StopShake()
        {
            if (p_currentShakeCoroutine == null) return;
            StopCoroutine(p_currentShakeCoroutine);

            if (p_currentNoise == null) return;
            p_currentNoise.m_AmplitudeGain = 0f;
            p_currentNoise.m_FrequencyGain = 0f;
        }

        /// <summary>
        /// Shake Coroutine
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShakeCo(float amplitude, float frequency, float duration)
        {
            p_currentNoise = (cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera)?.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (p_currentNoise is null) yield break;
            p_currentNoise.m_AmplitudeGain = amplitude;
            p_currentNoise.m_FrequencyGain = frequency;

            yield return new WaitForSeconds(duration);

            p_currentNoise.m_AmplitudeGain = 0f;
            p_currentNoise.m_FrequencyGain = 0f;
        }

    }
}