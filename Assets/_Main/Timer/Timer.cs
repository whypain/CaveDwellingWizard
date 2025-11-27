using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action<float> OnTimerUpdate;

    private float elapsedTime = 0f;
    private bool isRunning;


    public void Initialize()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public float Stop()
    {
        isRunning = false;
        return elapsedTime;
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            OnTimerUpdate?.Invoke(elapsedTime);
        }
    }
}
