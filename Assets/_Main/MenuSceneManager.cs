using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : Singleton<MenuSceneManager>
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] ParticleSystem menuParticleSystem;
    [SerializeField] LevelUI ui;

    public void StartGame()
    {
        ui.FadeOut(2, async () => {
            await SceneManager.LoadSceneAsync("Game");
        });
    }

}
