using System.Collections;
using Interactables;
using Managers;
using Plugins.Tools;
using UnityEngine;

namespace Tools
{
    [RequireComponent(typeof(Collider2D))]
    public class Elevator : MonoBehaviour, MMEventListener<LoadedEvent>
    {
        [SerializeField] private float minimumWaitTime;
        private bool p_isLoading;
        private Animator p_animator;

        private readonly int p_loadingProperty = Animator.StringToHash("isMoving");
        private void Awake()
        {
            p_animator = GetComponent<Animator>();
            DontDestroyOnLoad(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.transform.parent = transform;
            StartCoroutine(GoToNextFloor());
        }

        private void OnTriggerExit2D(Collider2D other) => other.transform.parent = null;

        private IEnumerator GoToNextFloor()
        {
            p_isLoading = true;
            float startTime = Time.time;

            /*
            SoundManager.Instance.Play("Elevator");
            SoundManager.Instance.StopBackgroundMusic();
            */
            
            p_animator.SetBool(p_loadingProperty, true);
            
            LevelLoadManager.Instance.LoadNextSceneAsync();

            yield return new WaitUntil(() => !p_isLoading);

            transform.position = FindObjectOfType<ElevatorPoint>().transform.position;

            while (Time.time - startTime < minimumWaitTime) yield return null;
            
            p_animator.SetBool(p_loadingProperty, false);
            
            /*
            SoundManager.Instance.Stop("Elevator");
            SoundManager.Instance.ResumeBackgroundMusic();
            */
        }

        private void OnEnable() => this.MMEventStartListening();

        private void OnDisable() => this.MMEventStopListening();

        public void OnMMEvent(LoadedEvent eventType) => p_isLoading = false;
    }
}
