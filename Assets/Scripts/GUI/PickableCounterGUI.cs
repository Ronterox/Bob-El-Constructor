using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickableCounterGUI : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        GameManager.instance.pickableCounterGUI = this;
        SetScore(GameManager.instance.gemsCount);
    }

    public void SetScore(int s)
    {
        if (countText != null) countText.text = s.ToString() + "x";
        else Debug.LogWarning("Count Text is null so it can't be update graphically");
    }
}
