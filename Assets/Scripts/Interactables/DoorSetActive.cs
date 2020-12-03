using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour {

    private Animator p_animator;
    private bool p_isOpen = false;
    private void Awake()
    {
        p_animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        p_isOpen = true;
        p_animator.SetBool("open", true);
    }

    public void CloseDoor()
    {
        p_isOpen = false;
        p_animator.SetBool("open", false);
    }

}
