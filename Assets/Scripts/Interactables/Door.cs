using Plugins.Tools;

namespace Interactables
{
    public class Door : Movable
    {
        public override void MoveForward()
        {
            base.MoveForward();
            SoundManager.Instance.Play("Door Open");
        }

        public override void MoveBackwards()
        {
            base.MoveBackwards();
            SoundManager.Instance.Play("Door Close");
        }
    }
}
