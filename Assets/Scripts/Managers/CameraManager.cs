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

    [SerializeField] private CinemachineBrain cinemachineBrain;

    private Coroutine p_currentShakeCoroutine;
    private CinemachineBasicMultiChannelPerlin p_currentNoise;

    [SerializeField] private CMCamera[] cinemachineVirtualCamera;

    /// <summary>
    /// Sets the priority of a camera to 'maximum'. So it changes to that camera
    /// </summary>
    /// <param name="id">Id of the camera to set priority to</param>
    public void SetPriority(string id)
    {
        foreach (CMCamera camera in cinemachineVirtualCamera)
        {
            if (camera.id.Equals(id)) camera.virtualCamera.Priority = 20;
            else camera.virtualCamera.Priority = 0;
        }
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
        if (p_currentShakeCoroutine != null)
        {
            StopCoroutine(p_currentShakeCoroutine);

            if (p_currentNoise != null)
            {
                p_currentNoise.m_AmplitudeGain = 0f;
                p_currentNoise.m_FrequencyGain = 0f;
            }
        }
    }

    /// <summary>
    /// Shake Coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShakeCo(float amplitude, float frequency, float duration)
    {
        p_currentNoise = (cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        p_currentNoise.m_AmplitudeGain = amplitude;
        p_currentNoise.m_FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        p_currentNoise.m_AmplitudeGain = 0f;
        p_currentNoise.m_FrequencyGain = 0f;
    }

}
