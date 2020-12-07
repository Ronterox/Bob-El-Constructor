using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    [SerializeField] private float timerTime;
    private float pf_timer = 0f;
    void Update()
    {
        if (pf_timer < 0) return;
        pf_timer -= Time.deltaTime;
        if (pf_timer < 0) pf_timer = 0;
        timerText.text = string.Format("{0:00}:{1:00}", pf_timer / 60 % 60, pf_timer % 60);
    }

    public void StartTimer()
    {
        pf_timer = timerTime;
    }
}
