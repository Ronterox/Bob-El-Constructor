using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Alarm {
    public GameObject[] alarm;
    public bool isOn;


}

public class ChangeAlarmColor : MonoBehaviour
{
    [SerializeField] private Color colorToTurnInto;
    [SerializeField] private Alarm[] p_alarm;
    // Start is called before the first frame update

    
    
   public void ChangeColor()
    {
        foreach (Alarm a in p_alarm)
        {
            if (!a.isOn)
            {
                foreach(GameObject g in a.alarm)
                {
                    SpriteRenderer rend;
                    rend = g.GetComponent<SpriteRenderer>();
                    rend.color = colorToTurnInto;
                }
                a.isOn = true;
                break;
               
            }
           
            
           
        }
     
    }
}
