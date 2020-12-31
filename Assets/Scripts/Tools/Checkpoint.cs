using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    [System.Serializable]
    public class ONCollision : UnityEvent { }

    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private ONCollision enterCheckpoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            GameManager.Instance.checkpoint = transform;
            gameObject.SetActive(false);
            enterCheckpoint.Invoke();
        }
    }
}
