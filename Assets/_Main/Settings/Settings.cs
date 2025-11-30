using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] List<GameObject> disableInMenu;

    private void Start()
    {
        settingsMenu.SetActive(false);
        InputSystem.actions["Player/ToggleSettings"].Enable();  
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
        if (LevelManager.Instance && !LevelManager.Instance.CanOpenSettings()) return;
        settingsMenu.SetActive(!settingsMenu.activeSelf);

        foreach (var obj in disableInMenu)
        {
            obj.SetActive(SceneManager.GetActiveScene().name != "Menu" && settingsMenu.activeSelf);
        }

        if (settingsMenu.activeSelf)
        {
            InputSystem.actions["Player/Move"].Disable();
            InputSystem.actions["Player/Attack"].Disable();
        }

        if (!settingsMenu.activeSelf)
        {
            InputSystem.actions["Player/Move"].Enable();
            InputSystem.actions["Player/Attack"].Enable();
        }
    }
}
