using System;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    private Timer timer;

    public void Initialize(Timer timer)
    {
        this.timer = timer;
        timer.OnTimerUpdate += SetTime;
    }

    void OnEnable()
    {
        timerText.text = "00:00.000";
    }

    public void OnDisable()
    {
        timer.OnTimerUpdate -= SetTime;
    }

    public void SetTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        timerText.text = string.Format("{0:D2}:{1:D2}.{2:D3}",
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds);
    }
}