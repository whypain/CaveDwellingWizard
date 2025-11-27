using System;
using System.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    public void FadeIn(float duration = 1f, Action onComplete = null)
    {
        fadeImage.raycastTarget = false;
        Tween.Alpha(fadeImage, 1f, 0f, duration).OnComplete(() => onComplete?.Invoke());
    }

    public void FadeOut(float duration = 1f, Action onComplete = null)
    {
        fadeImage.raycastTarget = true;
        Tween.Alpha(fadeImage, 0f, 1f, duration).OnComplete(() => onComplete?.Invoke());
    }

    public async Task FadeOutAsync(float duration = 1f)
    {
        fadeImage.raycastTarget = true;
        await Tween.Alpha(fadeImage, 0f, 1f, duration);
    }
}
