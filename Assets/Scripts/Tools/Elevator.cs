using System.Collections;
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

        private readonly int p_loadingProperty = Animator.StringToHash("Loading");
        private void Awake()
        {
            p_animator = GetComponent<Animator>();
            DontDestroyOnLoad(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other) => StartCoroutine(GoToNextFloor());

        private IEnumerator GoToNextFloor()
        {
            p_isLoading = true;
            float startTime = Time.time;
            
            /*
            SoundManager.Instance.Play("Elevator");
            SoundManager.Instance.StopBackgroundMusic();
            
            p_animator.SetBool(p_loadingProperty, true);
            */
            
            LevelLoadManager.Instance.LoadSceneAsync("TEST");
            
            yield return new WaitUntil(() => !p_isLoading);

            while (Time.time - startTime < minimumWaitTime) yield return null;
            
            /*
            p_animator.SetBool(p_loadingProperty, false);
            
            SoundManager.Instance.Stop("Elevator");
            SoundManager.Instance.ResumeBackgroundMusic();
            */
            Destroy(gameObject);
        }

        private void OnEnable() => this.MMEventStartListening();

        private void OnDisable() => this.MMEventStopListening();

        public void OnMMEvent(LoadedEvent eventType) => p_isLoading = false;
    }
}
