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

        public Transform checkpoint;

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
    }
}
