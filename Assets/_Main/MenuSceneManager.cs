using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : Singleton<MenuSceneManager>
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] ParticleSystem menuParticleSystem;
    [SerializeField] LevelUI ui;

    public void NewGame()
    {
        SaveLoadSystem.ClearSave();
        Continue();
    }

    public void Continue()
    {
        ui.FadeOut(2, async () => {
            await SceneManager.LoadSceneAsync("Game");
        });
    }

}
