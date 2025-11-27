using PrimeTween;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealthVignette : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] float min;
    [SerializeField] float max;

    Vignette vignette;
    Resource playerHealth;

    float current;

    TweenSettings<float> tweenVignette;

    void Awake()
    {
        volume.profile.TryGet(out vignette);
        vignette.intensity.value = min;
        current = min;

    }

    public void Initialize(Resource health)
    {
        playerHealth = health;
        playerHealth.OnChanged01 += UpdateVignette;
        UpdateVignette(playerHealth.Normalized);
    }

    void UpdateVignette(float healthNormalized)
    {
        if (vignette == null) return;
        // float newValue = Mathf.Lerp(min, max, 1f - healthNormalized);
        // vignette.intensity.value = newValue;

        tweenVignette = new TweenSettings<float>(
            startValue: current,
            endValue: Mathf.Lerp(min, max, 1f - healthNormalized),
            duration: 0.2f
        );

        Tween.Custom(tweenVignette, (value) =>
        {
            vignette.intensity.value = value;
            current = value;
        }); 
    }
}