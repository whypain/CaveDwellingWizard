using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    public void FadeIn(float duration = 1f, Action onComplete = null)
    {
        Tween.Alpha(fadeImage, 1f, 0f, duration).OnComplete(() => onComplete?.Invoke());
    }

    public void FadeOut(float duration = 1f, Action onComplete = null)
    {
        Tween.Alpha(fadeImage, 0f, 1f, duration).OnComplete(() => onComplete?.Invoke());
    }
}
