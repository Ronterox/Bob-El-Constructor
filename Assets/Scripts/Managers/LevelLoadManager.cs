using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[System.Serializable]
public class OnLoadEvent : UnityEvent {}
public class LevelLoadManager : PersistentSingleton<LevelLoadManager>
{
    [Header("Scenes")]
    [SerializeField] [Scene] private string[] additiveScenes;

    [Header("Events")]
    [SerializeField] private OnLoadEvent onLoadEvent;

    public void loadScene(string scene) 
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        onLoadEvent.Invoke();
    }

    public void loadNextScene() { loadScene(SceneManager.GetActiveScene().name); }
    public void loadAdditiveAsyncScenes() { foreach(string scene in additiveScenes) SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive); }
}
