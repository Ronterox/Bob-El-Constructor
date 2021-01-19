using GUI;
using Interactables;
using Plugins.Tools;
using UnityEngine;

namespace Managers
{
    public enum PlayerTool { None, Hammer, Brush, Static }

    public class GameManager : PersistentSingleton<GameManager>
    {
        public PlayerTool currentPlayerTool = PlayerTool.None;
        
        public bool gameIsPaused;

        public int gemsCount;
        [SerializeField] public PickableCounterGUI pickableCounterGUI;

        [Header("Game Events")]
        public OnPickableEvent onPickableEvent;
        public OnLoadEvent onLoadEvent;

        public void IncrementPickableGUI(int score)
        {
            gemsCount += score;
            if (pickableCounterGUI != null) pickableCounterGUI.SetScore(gemsCount);
        }

        public Settings GetSettings()
        {
            return new Settings();
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public Vector3Serializable checkpoint;
        public int gemsObtained;
        public string lastCameraID, lastLevel;
        public Settings settings;

        public PlayerData(Vector3Serializable checkpoint, int gemsObtained, string lastCameraID, string lastLevel, Settings settings)
        {
            this.checkpoint = checkpoint;
            this.gemsObtained = gemsObtained;
            this.lastCameraID = lastCameraID;
            this.lastLevel = lastLevel;
            this.settings = settings;
        }
    }

    [System.Serializable]
    public struct Vector3Serializable
    {
        public float x, y, z;

        public Vector3Serializable(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [System.Serializable]
    public struct Settings
    {
        public float generalVolume;
        public float uiVolume;
        public float sfxVolume;
        public float musicVolume;

        public bool fullScreen;
    }
}
