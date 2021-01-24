using Plugins.Tools;
using Tools;
using UnityEngine;

public class LightsOn : Checkpoint
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        SoundManager.Instance.Play("Lights");
    }
}
