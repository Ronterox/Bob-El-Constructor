using Plugins.Tools;
using UnityEngine;

namespace GUI
{
    public class SettingsMenu : MonoBehaviour
    {
        public void SetGeneralVolume(float volume) => SoundManager.Instance.SetVolume(volume, true);

        public void SetMusicVolume(float volume) => SoundManager.Instance.SetMusicVolume(volume,true);

        public void SetSFXVolume(float volume) => SoundManager.Instance.SetSfxVolume(volume,true);

        public void SetUIVolume(float volume) => SoundManager.Instance.SetUIVolume(volume,true);

        public void SetResolution() { }

        public void SetFullscreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;
    }
}
