using Plugins.Tools;
using UnityEngine;

namespace Managers
{
    public class PlaySfxOnStart : MonoBehaviour
    {
        [SerializeField] private string sound;

        private void Start() => SoundManager.Instance.Play(sound);
    }
}
