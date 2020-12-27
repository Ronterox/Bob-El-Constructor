using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    [System.Serializable]
    public class OnPickableEvent : UnityEvent<int> {}
    [RequireComponent(typeof(BoxCollider2D))]
    public class Pickable : MonoBehaviour 
    {
        [Tooltip("If you select this it will be destroyed instead of disabled")]
        [SerializeField] private bool destroyOnPick;
        [SerializeField] private int valueOnPick = 1;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            if (destroyOnPick) Destroy(gameObject);
            else gameObject.SetActive(false);
            GameManager.Instance.onPickableEvent.Invoke(valueOnPick);
        }
    }
}