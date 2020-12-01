using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : Singleton<LevelLoadManager>
{
    [Header("Scenes")]
    [SerializeField][Scene] private string[] additiveScenes;

    //Add on SceneLoad Event
    public void loadScene(string scene) { SceneManager.LoadScene(scene, LoadSceneMode.Single); }
    public void loadNextScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single); }
    public void loadAdditiveAsyncScenes() { foreach(string scene in additiveScenes) SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive); }
}
