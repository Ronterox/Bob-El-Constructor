namespace Interactables
{
    public class FinalDoor : Door
    {
        public int numberOfLightsToUnlock;
        private int lightsOn;

        public void TurnOnLight()
        {
            lightsOn++;
            if (lightsOn >= numberOfLightsToUnlock) MoveForward();
        }
    }
}
