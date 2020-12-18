﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


[System.Serializable]
public class OnLoadEvent : UnityEvent { }
public class LevelLoadManager : Singleton<LevelLoadManager>
{
    [Header("Scenes")]
    [SerializeField] [Scene] private string[] additiveScenes;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        GameManager.instance.onLoadEvent.Invoke();
    }

    /// <summary>
    /// Loads the next scene on the list of scenes
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        GameManager.instance.onLoadEvent.Invoke();
    }

    /// <summary>
    /// Loads all the additive scenes of the LevelLoadManager
    /// </summary>
    public void LoadAdditiveAsyncScenes() { foreach (string scene in additiveScenes) SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive); }

    /// <summary>
    /// Closes the Game or Stops the Inspector Game Window
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}


