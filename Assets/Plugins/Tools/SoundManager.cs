using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundItem
{
    public string id = "Sound";
    public AudioClip clip;

    public AudioMixerGroup audioMixerGroup;
    [Range(0f, 1f)]
    public float volume = 1f;

    public bool loop = false;
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
    private Dictionary<string, SoundItem> p_SoundEffects;

    [Header("Background Music")]
    [SerializeField] private SoundItem[] songs;
    private Dictionary<string, SoundItem> p_Songs;

    private string p_CurrentBackgroundMusic;

    protected override void Awake()
    {
        base.Awake();

        Initialize(p_Songs, songs);
        Initialize(p_SoundEffects, soundEffects);
    }

    /// <summary>
    /// Initializes the sound manager by instantiating AudioSource Components
    /// </summary>
    private void Initialize(Dictionary<string, SoundItem> sfxDict, SoundItem[] soundItems)
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
        }
    }

    /// <summary>
    /// Sets the general volume of the game Sounds and Music
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume) { audioMixer.SetFloat("General", VolumeToDecibels(generalVolume)); }

    /// <summary>
    /// Plays a background music.
    /// Only one background music can be active at a time.
    /// </summary>
    /// <param name="id">Audio clip id.</param>
    public IEnumerator _PlayBackgroundMusic(string id, float fadeDuration = 1f)
    {
        if (p_CurrentBackgroundMusic == id) yield break;

        yield return StartCoroutine(FadeMixerVolume(p_Songs[p_CurrentBackgroundMusic].audioMixerGroup.audioMixer, "BackgroundMusic_Volume", fadeDuration, 0f));

        p_CurrentBackgroundMusic = id;

        ResumeBackgroundMusic();

        yield return StartCoroutine(FadeMixerVolume(p_Songs[p_CurrentBackgroundMusic].audioMixerGroup.audioMixer, "BackgroundMusic_Volume", fadeDuration, musicVolume));
    }

    /// <summary>
    /// Fade mixer volume
    /// </summary>
    /// <param name="audioMixer"></param>
    /// <param name="exposedParam"></param>
    /// <param name="duration"></param>
    /// <param name="targetVolume"></param>
    /// <returns></returns>
    private IEnumerator FadeMixerVolume(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        audioMixer.GetFloat(exposedParam, out float currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);

        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        yield break;
    }

    /// <summary>
    /// Start Coroutine for play music
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBackgroundMusic(string id, float fadeDuration = 1) { StartCoroutine(_PlayBackgroundMusic(id, fadeDuration)); }

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
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(volume));
    }

    /// <summary>
    /// Mute SFX
    /// </summary>
    public void MuteSFX() { audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(0f)); }

    /// <summary>
    /// Unmute sfx
    /// </summary>
    public void UnMuteSFX() { audioMixer.SetFloat("SFX_Volume", VolumeToDecibels(sfxVolume)); }

    public void SetUIVolume(float volume)
    {
        uiVolume = volume;
        audioMixer.SetFloat("UI_SFX_Volume", VolumeToDecibels(volume));
    }

    /// <summary>
    /// Stop a background music
    /// </summary>
    public void StopBackgroundMusic() { p_Songs[p_CurrentBackgroundMusic].source.Stop(); }

    /// <summary>
    /// Stop a background music
    /// </summary>
    public void ResumeBackgroundMusic() { p_Songs[p_CurrentBackgroundMusic].source.Play(); }

    /// <summary>
    /// Pause background music
    /// </summary>
    public void PauseBackgroundMusic() { p_Songs[p_CurrentBackgroundMusic].source.Pause(); }

    /// <summary>
    /// Resume background music
    /// </summary>
    public void UnPauseBackgroundMusic() { p_Songs[p_CurrentBackgroundMusic].source.UnPause(); }

    /// <summary>
    /// Apply sound/music modification values
    /// </summary>
    /// <param name="musicValue"></param>
    /// <param name="sfxValue"></param>
    public void ApplySoundValues(float musicValue, float sfxValue)
    {
        SetMusicVolume(musicValue);
        SetSFXVolume(sfxValue);
    }

    /// <summary>
    /// Returns a decibel value as a volume value between 0 and 1
    /// </summary>
    /// <param name="dB"></param>
    /// <returns></returns>
    public float DecibelsToVolume(float dB) { return Mathf.Pow(10, dB / 20); }

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
        StopDictionarySounds(p_Songs);
        StopDictionarySounds(p_SoundEffects);
    }

    /// <summary>
    /// Stops a dictionary of list
    /// </summary>
    /// <param name="sfxDict"></param>
    private void StopDictionarySounds(Dictionary<string, SoundItem> sfxDict)
    {
        foreach (SoundItem sound in sfxDict.Values) sound.source.Stop();
    }

    /// <summary>
    /// Plays the sound by the same id
    /// </summary>
    /// <param name="id"></param>
    public void Play(string id, bool oneShot = false)
    {
        if (p_SoundEffects.ContainsKey(id))
        {
            if (oneShot) p_SoundEffects[id].source.Play();
            else p_SoundEffects[id].source.PlayOneShot(p_SoundEffects[id].clip);
        }
        else
        {
            if (oneShot) p_Songs[id].source.Play();
            else p_Songs[id].source.PlayOneShot(p_Songs[id].clip);
        }
    }

    /// <summary>
    /// Stops the sound by the same id
    /// </summary>
    /// <param name="id"></param>
    public void Stop(string id)
    {
        if (p_SoundEffects.ContainsKey(id)) p_SoundEffects[id].source.Stop();
        else p_Songs[id].source.Stop();
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
