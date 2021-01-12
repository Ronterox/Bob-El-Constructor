using Player;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ActivateGun : MonoBehaviour
    {
        public UnityEvent onGunPicked;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            other.GetComponent<Laser>().enabled = true;
            other.gameObject.SetActiveChildren();
            
            onGunPicked.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}
