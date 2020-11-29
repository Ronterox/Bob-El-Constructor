using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour {

    public TMP_Text timerText;
    public float timer = 0f;
    private float pf_mins;
    private bool pb_isStop;



    void Update()
    {
        if (pb_isStop)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
            pb_isStop = true;
        }
        timerText.text = string.Format("{0:00}:{1:00}", timer / 60 % 60, timer%60);

    }

   
}
