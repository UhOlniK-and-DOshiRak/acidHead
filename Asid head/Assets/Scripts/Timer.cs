using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public int minutes;
    public int sec;
    int totalSeconds = 0;
    int TOTAL_SECONDS = 0;
    public float waitfor;
    
    void Start()
    {
        timeText.color = Color.green;
        waitfor = 1f;
        timeText.text = minutes + ":" + sec;
        if (minutes > 0)
            totalSeconds += minutes * 60;
        if (sec > 0)
            totalSeconds += sec;
        TOTAL_SECONDS = totalSeconds;
        StartCoroutine(second());
    }

    IEnumerator second()
    {
        yield return new WaitForSeconds(waitfor);
        if (sec > 0)
            sec--;
        if (sec == 0 && minutes != 0)
        {
            sec = 59;
            minutes--;
        }
        timeText.text = minutes + ":" + sec;
        StartCoroutine(second());
    }

    void Update()
    {
        if (minutes == 0 && sec == 30)
        {
            timeText.color = Color.red;
        }

        if (sec == 0 && minutes == 0 && DataHolder.dayStarted)
        {
            //StopCoroutine(second());
            Time.timeScale = 1.0f;
            FindObjectOfType<GameController>().EndDay();
        }
    }

    public void SetWaitFor(float amount)
    {
        waitfor = amount;
    }
}
