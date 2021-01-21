using Managers;

namespace GUI
{
    public class FinalScreen : Timer
    {
        protected override void Awake()
        {
            base.Awake();
            StartTimer();
            onTimerEnd.AddListener(GoBackMenu);
        }

        private void GoBackMenu() => LevelLoadManager.Instance.LoadScene("MAIN MENU");
    }
}
