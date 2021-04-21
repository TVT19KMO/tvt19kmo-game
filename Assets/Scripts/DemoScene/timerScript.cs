using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    public Text timertext;
    public float timerValue = 0;
    public bool timerIsRunning = false;
    public void TimerStart()
    {
        timerIsRunning = true;
    }
    public void TimerStop()
    {
        timerIsRunning = false;
        timerValue = 0;
    }
    void Update()
    {
        if(timerIsRunning)
        {
            timerValue += Time.deltaTime;
            timertext.text = timerValue.ToString();
        }
    }
}
