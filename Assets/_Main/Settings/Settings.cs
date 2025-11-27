using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;

    private void Start()
    {
        settingsMenu.SetActive(false);
    }

    private void OnEnable()
    {
        InputSystem.actions["Player/ToggleSettings"].performed += OnToggleSettings;
    }

    private void OnDisable()
    {
        InputSystem.actions["Player/ToggleSettings"].performed -= OnToggleSettings;
    }

    private void OnToggleSettings(InputAction.CallbackContext _)
    {
        if (LevelManager.Instance.IsLevelOver || LevelManager.Instance.IsTransitioning) return;
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
}
