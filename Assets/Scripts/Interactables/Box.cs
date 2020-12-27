using Plugins.Tools;
using UnityEngine;

namespace Interactables
{
    public class Box : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private float hitSoundDelay;
        private float p_lastHitTime;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!(Time.time - p_lastHitTime >= hitSoundDelay)) return;
            p_lastHitTime = Time.time;
            SoundManager.Instance.Play("Box Hit");
        }
    }
}
