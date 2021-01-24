using System.Collections;
using Managers;
using Plugins.Tools;
using UnityEngine;

public class TimeFinalScreen : MonoBehaviour
{
    [SerializeField] private float timeToWait = 1f;

    public void StartTimeCall() => StartCoroutine(TimeCall());

    private IEnumerator TimeCall()
    {
        yield return new WaitForSeconds(timeToWait);
        LevelLoadManager.Instance.LoadNextScene();
    }

    public void JumpScare() => SoundManager.Instance.Play("JUMPSCARE");
}
