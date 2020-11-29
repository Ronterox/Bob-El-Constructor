using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GemCounter.gemCounter.RaiseScore(1);
        Destroy(transform.parent.gameObject);
    }
}
