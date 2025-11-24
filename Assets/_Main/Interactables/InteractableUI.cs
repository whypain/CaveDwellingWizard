using PrimeTween;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    public void Show()
    {
        Tween.Alpha(canvasGroup, startValue: 0f, endValue: 1f, duration: 0.5f);
    }

    public void Hide()
    {
        Tween.Alpha(canvasGroup, startValue: 1f, endValue: 0f, duration: 0.5f);
    }

    public void HideImmediate()
    {
        canvasGroup.alpha = 0f;
    }
}