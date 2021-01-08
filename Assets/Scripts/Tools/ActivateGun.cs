using Player;
using Plugins.Tools;
using UnityEngine;

namespace Tools
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ActivateGun : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            other.GetComponent<Laser>().enabled = true;
            other.gameObject.SetActiveChildren();
            
            gameObject.SetActive(false);
        }
    }
}
