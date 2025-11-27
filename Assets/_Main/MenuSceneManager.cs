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

    public void StartGame()
    {
        ui.FadeOut(2, async () => {
            BGMManager.Instance.PlayLevelBGM();
            await Task.Delay(1000);
            await SceneManager.LoadSceneAsync("Game");
        });
    }

}
