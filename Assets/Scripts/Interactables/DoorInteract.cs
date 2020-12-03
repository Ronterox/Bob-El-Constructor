using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour {

    [SerializeField] private GameObject doorGameobject;

    private iDoor door;

    private void Awake()
    {
        door = doorGameobject.GetComponent<iDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      door.OpenDoor();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      door.CloseDoor();
    }



}
