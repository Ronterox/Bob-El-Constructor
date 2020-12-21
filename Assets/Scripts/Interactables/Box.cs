using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private float hitSoundDelay;
    private float pf_lastHitTime;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if ((Time.time - pf_lastHitTime) >= hitSoundDelay)
        {
            pf_lastHitTime = Time.time;
            SoundManager.instance.Play("Box Hit");
        } 
    }
}
