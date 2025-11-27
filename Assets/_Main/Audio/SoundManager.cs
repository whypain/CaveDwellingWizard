using System;
using System.Threading.Tasks;
using PrimeTween;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    protected override void AfterSingletonCheck()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public async void TransitionBGM(AudioClip newClip, float duration = 1f, float volume = 1f)
    {
        if (newClip == null) return;
        if (newClip == bgmSource.clip) return;

        if (bgmSource.clip != null)
        {
            await Tween.AudioVolume(bgmSource, bgmSource.volume, 0f, duration / 2f);
            await Task.Delay(1000);
        }

        bgmSource.clip = newClip;
        bgmSource.volume = 0f;
        bgmSource.loop = true;
        bgmSource.Play();

        await Tween.AudioVolume(bgmSource, 0f, volume, duration / 2f);
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, Vector2? position = null)
    {
        if (clip == null) return;

        if (position.HasValue)
        {
            AudioSource.PlayClipAtPoint(clip, position.Value, volume);
            return;
        }
        sfxSource.PlayOneShot(clip, volume);
    }

    void OnDestroy()
    {
        bgmSource.Stop();
        sfxSource.Stop();
    }
}
