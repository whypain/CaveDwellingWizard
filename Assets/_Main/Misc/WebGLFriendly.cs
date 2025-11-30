using System.Threading.Tasks;
using UnityEngine;

public static class WebGLFriendly
{
    public static async Task Delay(int millisecondsDelay)
    {
        float timeElapsed = 0f;
        while (timeElapsed < millisecondsDelay / 1000f)
        {
            await Task.Yield();
            timeElapsed += Time.deltaTime;
        }
    }
}