using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : Singleton<MenuSceneManager>
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] ParticleSystem menuParticleSystem;
    [SerializeField] LevelUI ui;

    private void Start()
    {
        ui.FadeIn(2);
    }

    public async void StartGame()
    {
        await ui.FadeOutAsync(2);

        BGMManager.Instance.PlayLevelBGM();
        await WebGLFriendly.Delay(1000);
        SceneManager.LoadScene("Game");
    }

}
