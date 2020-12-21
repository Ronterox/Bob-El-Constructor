using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Movable 
{
    public override void MoveForward()
    {
        base.MoveForward();
        SoundManager.instance.Play("Door Open");
    }

    public override void MoveBackwards()
    {
        base.MoveBackwards();
        SoundManager.instance.Play("Door Close");
    }
}
