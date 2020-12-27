using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    [System.Serializable]
    public class OnButtonEvent : UnityEvent { }

    public class Button : MonoBehaviour
    {
        [SerializeField] private OnButtonEvent onButtonEventEnter;
        [SerializeField] private OnButtonEvent onButtonEventExit;
        private int p_collisionCounter;
        private Animator p_spriteAnimator;
        private readonly int p_isPressed = Animator.StringToHash("IsPressed");

        private void Awake() => p_spriteAnimator = gameObject.GetComponent<Animator>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (p_collisionCounter == 0)
            {
                SoundManager.Instance.Play("Button", true);
                p_spriteAnimator.SetBool(p_isPressed, true);
                onButtonEventEnter.Invoke();
            }
            p_collisionCounter++;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            p_collisionCounter--;
            if (p_collisionCounter != 0) return;
            p_spriteAnimator.SetBool(p_isPressed, false);
            onButtonEventExit.Invoke();
        }
    }
}
