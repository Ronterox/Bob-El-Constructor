using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour 
{
    [SerializeField] private Scene[] additiveScenes;

    //Add on SceneLoad Event
    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public void loadAdditiveScenes()
    {
        //Check if is load
        foreach(Scene scene in additiveScenes)
        {
            SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Additive);
        }
    }
}
