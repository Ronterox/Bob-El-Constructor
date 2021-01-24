using UnityEngine;

public class Spawn : MonoBehaviour
{
    private void Start() => FindObjectOfType<Player.Player>().transform.position = transform.position;
}
