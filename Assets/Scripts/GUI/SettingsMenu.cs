using System.Collections.Generic;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GUI
{
    [System.Serializable]
    public struct Settings
    {
        public float generalVolume;
        public float uiVolume;
        public float sfxVolume;
        public float musicVolume;

        public bool fullScreen;
        public int resolution;
    }
    
    public class SettingsMenu : MonoBehaviour
    {
        private Settings settings;

        private Resolution[] resolutions;

        [SerializeField] private Slider generalVolume;
        [SerializeField] private Slider uiVolume;
        [SerializeField] private Slider sfxVolume;
        [SerializeField] private Slider musicVolume;

        [SerializeField] private Toggle fullscreenToggle;

        [SerializeField] private TMP_Dropdown resolutionDropdown;

        private const string SAVED_FILENAME = "settings.cfg";
        private const string SAVED_FOLDERNAME = "SavedStates";
        private void Start()
        {
            SetSystemResolutions();
            CheckForSavedSettings();
        }

        private void SetSystemResolutions()
        {
            resolutions = Screen.resolutions;
            
            var resolutionsList = new List<string>();
            var resolutionIndex = 0;
            
            foreach (Resolution resolution in resolutions)
            {
                resolutionsList.Add(resolution.width + " x " + resolution.height);
                if (resolution.Equals(Screen.currentResolution)) resolutionIndex = resolutionsList.Count - 1;
            }
            resolutionDropdown.AddOptions(resolutionsList);
            resolutionDropdown.value = resolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void CheckForSavedSettings()
        {
            if (SaveLoadManager.SaveExists(SAVED_FILENAME, SAVED_FOLDERNAME))
            {
                settings = SaveLoadManager.Load<Settings>(SAVED_FILENAME, SAVED_FOLDERNAME);
                UpdateGameObjects();
            }
            else
            {
                SoundManager soundManager = SoundManager.Instance;
                settings = new Settings
                {
                    fullScreen = fullscreenToggle.isOn,

                    generalVolume = soundManager.generalVolume,
                    musicVolume = soundManager.musicVolume,
                    sfxVolume = soundManager.sfxVolume,
                    uiVolume = soundManager.uiVolume
                };
                SaveSettings();
            }
        }

        private void UpdateGameObjects()
        {
            fullscreenToggle.isOn = settings.fullScreen;

            generalVolume.value = settings.generalVolume;
            uiVolume.value = settings.uiVolume;
            sfxVolume.value = settings.sfxVolume;
            musicVolume.value = settings.musicVolume;

            resolutionDropdown.value = settings.resolution;
        }

        public void SetGeneralVolume(float volume) => SoundManager.Instance.SetVolume(volume, true);

        public void SetMusicVolume(float volume) => SoundManager.Instance.SetMusicVolume(volume, true);

        public void SetSFXVolume(float volume) => SoundManager.Instance.SetSfxVolume(volume, true);

        public void SetUIVolume(float volume) => SoundManager.Instance.SetUIVolume(volume, true);

        public void SetResolution(int resolutionIndex)
        {
            Resolution currentResolution = resolutions[resolutionIndex];
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
        }

        private void OnDisable() => SaveSettings();

        public void SetFullscreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;

        private void SaveSettings() => SaveLoadManager.Save(settings, SAVED_FILENAME, SAVED_FOLDERNAME);
    }
}
