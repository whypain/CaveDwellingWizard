using System.Linq;
using UnityEngine;

public class MenuSceneManager : Singleton<MenuSceneManager>
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] ParticleSystem menuParticleSystem;

    public void SetMenuActive(bool isActive)
    {
        menuPanel.SetActive(isActive);
        if (isActive)
        {
            menuParticleSystem.Play();
        }
        else
        {
            menuParticleSystem.Stop();
            menuParticleSystem.Clear();
        }
    }

}
