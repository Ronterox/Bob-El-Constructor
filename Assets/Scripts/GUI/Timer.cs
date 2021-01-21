using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GUI
{
    public class Timer : MonoBehaviour
    {
        public UnityEvent onTimerEnd;
        public TextMeshProUGUI timerText;

        [SerializeField] private float timerTime;
        private float p_timer;

        private bool hasText;

        protected virtual void Awake() => hasText = timerText != null;
        private void Update()
        {
            if (p_timer < 0) return;
            p_timer -= Time.deltaTime;
            if (p_timer < 0)
            {
                p_timer = 0;
                onTimerEnd.Invoke();
            }
            if(hasText) timerText.text = $"{p_timer / 60 % 60:00}:{p_timer % 60:00}";
        }

        public void StartTimer() => p_timer = timerTime;
    }
}
