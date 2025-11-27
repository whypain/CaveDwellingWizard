using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : Singleton<BGMManager>
{
    [SerializeField] AudioClip titleBGM;
    [SerializeField] AudioClip levelBGM;
    [SerializeField] AudioClip levelFailedBGM;
    [SerializeField] AudioClip levelCompletedBGM;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(titleBGM);
    }

    public void PlayTitleBGM()
    {
        SoundManager.Instance.TransitionBGM(titleBGM);
    }

    public void PlayLevelBGM()
    {
        SoundManager.Instance.TransitionBGM(levelBGM);
    }

    public void PlayLevelFailedBGM()
    {
        SoundManager.Instance.TransitionBGM(levelFailedBGM);
    }

    public void PlayLevelCompletedBGM()
    {
        SoundManager.Instance.TransitionBGM(levelCompletedBGM);
    }
}