using System.Collections;
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

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        GameManager.instance.onLoadEvent.Invoke();
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        GameManager.instance.onLoadEvent.Invoke();
    }
    public void loadAdditiveAsyncScenes() { foreach (string scene in additiveScenes) SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive); }

    public void OptionQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}


