using Managers;
using Plugins.Tools;

namespace Interactables
{
    public class FinalDoor : Door
    {
        public int numberOfLightsToUnlock;
        private int lightsOn;

        private void Start() => SoundManager.Instance.Play("Drums Suspense");
        public void TurnOnLight()
        {
            lightsOn++;
            if (lightsOn >= numberOfLightsToUnlock) MoveForward();
            SoundManager.Instance.Play("Mudos");
        }
        public void EndGame() => LevelLoadManager.Instance.LoadNextScene();

        public void LoadMenu() => LevelLoadManager.Instance.LoadScene("MAIN MENU");
    }
}
