using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour 
{
    /// <summary>
    /// Loads the game last saved Scene
    /// </summary>
    public void StartGame()
    {
        //Need To later depend on save file
        LevelLoadManager.instance.LoadScene("Level 1");
        LevelLoadManager.instance.LoadAdditiveAsyncScenes();
    }

    /// <summary>
    /// Loads a specific scene
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene) { LevelLoadManager.instance.LoadScene(scene); }

    /// <summary>
    /// Closes or stops the game
    /// </summary>
    public void QuitApplication() { LevelLoadManager.instance.QuitGame(); }
}
