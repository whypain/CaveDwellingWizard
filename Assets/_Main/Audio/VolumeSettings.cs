using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider master;
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;

    private void Start()
    {
        master.onValueChanged.AddListener(OnMasterVolumeChanged);
        bgm.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfx.onValueChanged.AddListener(OnSFXVolumeChanged);

        LoadSettings();
    }

    private void OnDestroy()
    {
        master.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        bgm.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfx.onValueChanged.RemoveListener(OnSFXVolumeChanged);

        SaveSettings();
    }

    private void OnMasterVolumeChanged(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }

    private void OnBGMVolumeChanged(float value)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    private void OnSFXVolumeChanged(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", master.value);
        PlayerPrefs.SetFloat("BGMVolume", bgm.value);
        PlayerPrefs.SetFloat("SFXVolume", sfx.value);
        PlayerPrefs.Save();
    }   

    private void LoadSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        master.value = masterVolume;
        OnMasterVolumeChanged(masterVolume);

        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.2f);
        bgm.value = bgmVolume;
        OnBGMVolumeChanged(bgmVolume);

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfx.value = sfxVolume;
        OnSFXVolumeChanged(sfxVolume);
    }
}
