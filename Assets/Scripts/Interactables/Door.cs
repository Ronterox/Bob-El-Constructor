using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    private Animator p_animator;
    private void Awake() { p_animator = GetComponent<Animator>(); }
    public void OpenDoor() { p_animator.SetBool("open", true); }
    public void CloseDoor() { p_animator.SetBool("open", false); }
}
