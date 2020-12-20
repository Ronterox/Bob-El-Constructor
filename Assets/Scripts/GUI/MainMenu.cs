using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour 
{
    public GameObject startgame, quitgame, loadscene;
    /// <summary>
    /// Loads the game last saved Scene
    /// </summary>
    public void StartGame()
    {
        //Need To later depend on save file
        LevelLoadManager.instance.LoadScene("Level 1");
        LevelLoadManager.instance.LoadAdditiveAsyncScenes();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startgame);
    }

    /// <summary>
    /// Loads a specific scene
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene)
    {
        LevelLoadManager.instance.LoadScene(scene);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(loadscene);
    }

    /// <summary>
    /// Closes or stops the game
    /// </summary>
    public void QuitApplication()
    {
        LevelLoadManager.instance.QuitGame();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitgame);
    }
}
