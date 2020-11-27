using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCounter : MonoBehaviour {

    public static GemCounter gemCounter;
    int score = 0;
    public Text scoretext;
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        scoretext = GameObject.Find("Gems Counter").GetComponent<Text>();
        scoretext.text = score.ToString();
		
	}

    public void RaiseScore(int s)
    {
        score += s;
        scoretext.text = score.ToString();
    }
}
