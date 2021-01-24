using Managers;
using Managers.CameraManager;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    [System.Serializable]
    public class OnCollision : UnityEvent { }

    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private OnCollision enterCheckpoint;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            SaveCheckpointState();
            gameObject.SetActive(false);
            enterCheckpoint.Invoke();
        }

        /// <summary>
        /// Save the checkpoint state in bas of GameManager, CameraManager and LevelLoadManager values
        /// </summary>
        private void SaveCheckpointState() =>
            SaveLoadManager.Save(
                new PlayerData(SerializeVector3(transform.position), 
                               GameManager.Instance.gemsCount, 
                               CameraManager.Instance.currentCameraID, 
                               LevelLoadManager.Instance.GetSceneName())
                
                , $"saved_state_v{Application.version}","SavedStates");

        private Vector3Serializable SerializeVector3(Vector3 toSerialize) => new Vector3Serializable(toSerialize.x, toSerialize.y, toSerialize.z);
    }
}
