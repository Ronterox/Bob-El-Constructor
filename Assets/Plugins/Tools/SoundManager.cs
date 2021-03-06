﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Plugins.Tools
{
    [System.Serializable]
    public class SoundItem
    {
        public string id = "Sound";
        public AudioClip clip;

        public AudioMixerGroup audioMixerGroup;
        [Range(0f, 1f)]
        public float volume = 1f;

        public bool loop;
        [Range(-3f, 3f)]
        public float pitch = 1f;

        [HideInInspector]
        public AudioSource source;
    }

    public class SoundManager : PersistentSingleton<SoundManager>
    {
        [Header("Values")]
        [Range(0, 1)]
        public float generalVolume = 1f;
        [Range(0, 1)]
        public float musicVolume = 0.3f;
        [Range(0, 1)]
        public float sfxVolume = 1f;
        [Range(0, 1)]
        public float uiVolume = 1f;

        [Header("Mixer")]
        public AudioMixer audioMixer;

        [Header("Sound Effects")]
        [SerializeField] private SoundItem[] soundEffects;
        private Dictionary<string, SoundItem> p_soundEffects;

        [Header("Background Music")]
        [SerializeField] private SoundItem[] songs;
        private Dictionary<string, SoundItem> p_songs;

        private string p_currentBackgroundMusic;

        protected override void Awake()
        {
            base.Awake();

            UpdateVolumeValues();

            p_soundEffects = new Dictionary<string, SoundItem>();
            p_songs = new Dictionary<string, SoundItem>();

            Initialize(p_songs, songs);
            Initialize(p_soundEffects, soundEffects);
        }

        /// <summary>
        /// Updates all volume values on the mixer
        /// </summary>
        private void UpdateVolumeValues()
        {
            SetVolume(generalVolume);
            SetMusicVolume(musicVolume);
            SetSfxVolume(sfxVolume);
            SetUIVolume(uiVolume);
        }

        /// <summary>
        /// Initializes the sound manager by instantiating AudioSource Components
        /// </summary>
        private void Initialize(Dictionary<string, SoundItem> sfxDict, IEnumerable<SoundItem> soundItems)
        {
            foreach (SoundItem item in soundItems) sfxDict.Add(item.id, item);

            foreach (SoundItem sound in sfxDict.Values)
            {
                sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
                sound.source.clip = sound.clip;
                sound.source.loop = sound.loop;
                sound.source.pitch = sound.pitch;
                sound.source.volume = sound.volume;
                sound.source.playOnAwake = false;
            }
        }

        /// <summary>
        /// Sets the general volume of the game Sounds and Music
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolume(float volume) => audioMixer.SetFloat("General", VolumeToDecibels(generalVolume));

        /// <summary>
        /// Plays a background music.
        /// Only one background music can be active at a time.
        /// </summary>
        /// <param name="id">Audio clip id.</param>
        /// <param name="fadeDuration">fade duration between song changes</param>
        public IEnumerator _PlayBackgroundMusic(string id, float fadeDuration = 1f)
        {
            if (p_currentBackgroundMusic == id) yield break;

            yield return StartCoroutine(FadeMixerVolume(p_songs[p_currentBackgroundMusic].audioMixerGroup.audioMixer, "BackgroundMusic_Volume", fadeDuration, 0f));

            p_currentBackgroundMusic = id;

            ResumeBackgroundMusic();

            yield return StartCoroutine(FadeMixerVolume(p_songs[p_currentBackgroundMusic].audioMixerGroup.audioMixer, "BackgroundMusic_Volume", fadeDuration, musicVolume));
        }

        /// <summary>
        /// Fade mixer volume
        /// </summary>
        /// <param name="mixer"></param>
        /// <param name="exposedParam"></param>
        /// <param name="duration"></param>
        /// <param name="targetVolume"></param>
        /// <returns></returns>
        private IEnumerator FadeMixerVolume(AudioMixer mixer, string exposedParam, float duration, float targetVolume)
        {
            float currentTime = 0;
            mixer.GetFloat(exposedParam, out float currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);

            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
                mixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
                yield return null;
            }
        }

        /// <summary>
        /// Start Coroutine for playing music
        /// </summary>
        /// <param name="id">Id of the song to play</param>
        /// <param name="fadeDuration">Optional duration of the fade on changing songs</param>
        public void PlayBackgroundMusic(string id, float fadeDuration = 1) => StartCoroutine(_PlayBackgroundMusic(id, fadeDuration));

        /// <summary>
        /// Set music volume
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicVolume(float volume)
        {
            musicVolume = volume;
            audioMixer.SetFloat("BackgroundMusic_Volume", VolumeToDecibels(volume));
        }

        /// <summary>
        /// Set SFX volume
        /// </summary>
        /// <param name="volume"></param>
        public void SetSfxVolume(float volume)
        {
            sfxVolume = volume;
            audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(volume));
        }

        /// <summary>
        /// Mute SFX
        /// </summary>
        public void MuteSfx() => audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(0f));

        /// <summary>
        /// Unmute sfx
        /// </summary>
        public void UnMuteSfx() => audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(sfxVolume));

        public void SetUIVolume(float volume)
        {
            uiVolume = volume;
            audioMixer.SetFloat("UI_SFX_Volume", VolumeToDecibels(volume));
        }

        /// <summary>
        /// Stop a background music
        /// </summary>
        public void StopBackgroundMusic() => p_songs[p_currentBackgroundMusic].source.Stop();

        /// <summary>
        /// Stop a background music
        /// </summary>
        public void ResumeBackgroundMusic() => p_songs[p_currentBackgroundMusic].source.Play();

        /// <summary>
        /// Pause background music
        /// </summary>
        public void PauseBackgroundMusic() => p_songs[p_currentBackgroundMusic].source.Pause();

        /// <summary>
        /// Resume background music
        /// </summary>
        public void UnPauseBackgroundMusic() => p_songs[p_currentBackgroundMusic].source.UnPause();

        /// <summary>
        /// Returns a decibel value as a volume value between 0 and 1
        /// </summary>
        /// <param name="dB"></param>
        /// <returns></returns>
        public float DecibelsToVolume(float dB) => Mathf.Pow(10, dB / 20);

        /// <summary>
        /// Returns the decibels value of a volume between 0 and 1
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public float VolumeToDecibels(float volume) { if (volume > 0) return Mathf.Log10(volume) * 20; else return -80f; }

        /// <summary>
        /// Stops all current sfx
        /// </summary>
        public void StopAllSfx()
        {
            StopDictionarySounds(p_songs);
            StopDictionarySounds(p_soundEffects);
        }

        /// <summary>
        /// Stops a dictionary of list
        /// </summary>
        /// <param name="sfxDict"></param>
        private void StopDictionarySounds(Dictionary<string, SoundItem> sfxDict) { foreach (SoundItem sound in sfxDict.Values) sound.source.Stop(); }

        /// <summary>
        /// Plays the sound by the same id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oneShot">True if the sound can't be overlapped and can't be stopped</param>
        public void Play(string id, bool oneShot = false)
        {
            if (p_soundEffects.ContainsKey(id))
            {
                if (oneShot) p_soundEffects[id].source.Play();
                else p_soundEffects[id].source.PlayOneShot(p_soundEffects[id].clip);
            }
            else
            {
                if (oneShot) p_songs[id].source.Play();
                else p_songs[id].source.PlayOneShot(p_songs[id].clip);
            }
        }

        /// <summary>
        /// Stops the sound by the same id
        /// </summary>
        /// <param name="id"></param>
        public void Stop(string id)
        {
            if (p_soundEffects.ContainsKey(id)) p_soundEffects[id].source.Stop();
            else p_songs[id].source.Stop();
        }

        /// <summary>
        /// Change volume to half for menus
        /// </summary>
        public void OpenMenuVolume()
        {
            audioMixer.SetFloat("BackgroundMusic_Volume", VolumeToDecibels(musicVolume / 2));
            audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(sfxVolume / 2));
        }

        /// <summary>
        /// Change Volume to default for closed menus
        /// </summary>
        public void CloseMenuVolume()
        {
            audioMixer.SetFloat("BackgroundMusic_Volume", VolumeToDecibels(musicVolume));
            audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(sfxVolume));
        }
    }
}