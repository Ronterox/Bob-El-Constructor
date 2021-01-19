using Plugins.Tools;
using UnityEngine;

namespace GUI
{
    public class SettingsMenu : MonoBehaviour
    {
        public void SetGeneralVolume(float volume) => SoundManager.Instance.SetVolume(volume);

        public void SetMusicVolume(float volume) => SoundManager.Instance.SetMusicVolume(volume);

        public void SetSFXVolume(float volume) => SoundManager.Instance.SetSfxVolume(volume);

        public void SetUIVolume(float volume) => SoundManager.Instance.SetUIVolume(volume);

        public void SetResolution() { }

        public void SetFullscreen(bool fullscreen) { }
    }
}
