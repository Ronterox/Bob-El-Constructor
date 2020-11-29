using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GemCounter : MonoBehaviour {

    public static GemCounter gemCounter;
    int score = 0;
    public TMP_Text scoretext;
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        scoretext = GameObject.Find("Gems Counter").GetComponent<TMP_Text>();
        scoretext.text = score+"x";
		
	}

    public void RaiseScore(int s)
    {
        score += s;
        scoretext.text = score+"x";
    }
}
