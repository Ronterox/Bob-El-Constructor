using Managers;
using TMPro;
using UnityEngine;

namespace GUI
{
    public class PickableCounterGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;

        private void Awake()
        {
            GameManager.Instance.pickableCounterGUI = this;
            SetScore(GameManager.Instance.gemsCount);
        }

        public void SetScore(int s)
        {
            if (countText != null) countText.text = s + "x";
            else Debug.LogWarning("Count Text is null so it can't be update graphically");
        }
    }
}
