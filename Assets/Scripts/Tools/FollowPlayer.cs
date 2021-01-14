using UnityEngine;

namespace Tools
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
    
        private Transform p_transformPlayer;
        private bool p_isFollowing;

        private void Awake() => p_transformPlayer = GameObject.FindWithTag("Player").transform;

        private void Update()
        {
            if (p_isFollowing) transform.position = Vector3.MoveTowards(transform.position, p_transformPlayer.position, movementSpeed * Time.deltaTime);
        }

        public void StartFollowing() => p_isFollowing = true;
    
        public void StopFollowing() => p_isFollowing = false;
    }
}
