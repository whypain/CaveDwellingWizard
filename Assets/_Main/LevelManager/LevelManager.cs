using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public Level CurrentLevel => currentLevel;
    public Player Player => player;
    public CameraManager CamManager => camManager;
    public bool IsLevelOver => isLevelOver;
    public bool IsTransitioning => isTransitioning;

    [Header("Levels")]
    [SerializeField] List<Level> levels;
    [SerializeField] string levelCompleteText = "Level Complete";
    [SerializeField] string levelFailedText = "Level Failed";

    [Header("References")]
    [SerializeField] LevelUI ui;
    [SerializeField] CollectibleUIManager collectibleUIManager;
    [SerializeField] Timer levelTimer;
    [SerializeField] TimerUI levelTimerUI;
    [SerializeField] GameOverUI gameOverUI;

    private CameraManager camManager;
    private Player player;
    private Level currentLevel;
    private int currentLevelIndex;
    private float timeTaken;
    private bool isLevelOver;
    private bool isTransitioning;


    private void Start()
    {
        LoadLevel(0);
    }

    public async void LoadLevel(int index)
    {
        isTransitioning = true;
        Debug.Log("Loading level index: " + index);

        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.actions["Player/Attack"].Enable();

        if (currentLevel != null) throw new System.Exception("There is already a level loaded");
        isLevelOver = false;

        Level spawnedLevel = Instantiate(levels[index], transform);
        spawnedLevel.name = levels[index].name;
        spawnedLevel.transform.position = Vector3.zero;
        currentLevel = spawnedLevel;
        currentLevelIndex = index;

        InitializeSystems();

        await ui.FadeInAsync();
        isTransitioning = false;
    }

    private void InitializeSystems()
    {
        player ??= FindFirstObjectByType<Player>(FindObjectsInactive.Include) ?? throw new System.Exception("Player not found in scene");
        camManager ??= FindFirstObjectByType<CameraManager>(FindObjectsInactive.Include) ?? throw new System.Exception("CameraManager not found in scene");

        player.Initialize();
        levelTimer.Initialize();

        camManager.Initialize(player);
        collectibleUIManager.Initialize(player.CollectibleManager);
        levelTimerUI.Initialize(levelTimer);
    }

    [ContextMenu("Complete Level")]
    public void CompleteLevel()
    {
        if (isLevelOver) return;
        isLevelOver = true;

        InputSystem.DisableDevice(Keyboard.current);
        InputSystem.actions["Player/Attack"].Disable();

        player.OnCompleteLevel();
        timeTaken = levelTimer.Stop();

        gameOverUI.Initialize(player.CollectibleManager, levelCompleteText, timeTaken);
        if (currentLevelIndex + 1 < levels.Count)
            gameOverUI.InitNextLevelButton();
    }

    [ContextMenu("Fail Level")]
    public void FailLevel()
    {
        if (isLevelOver) return;
        isLevelOver = true;

        InputSystem.DisableDevice(Keyboard.current);
        InputSystem.actions["Player/Attack"].Disable();

        // player.OnFailLevel();
        timeTaken = levelTimer.Stop();

        gameOverUI.Initialize(player.CollectibleManager, levelFailedText, timeTaken);
    }

    [ContextMenu("Exit Level")]
    private async Task OnExit()
    {
        isTransitioning = true;
        await ui.FadeOutAsync();
        isTransitioning = false;

        timeTaken = 0f;
        gameOverUI.gameObject.SetActive(false);

        player = null;
        camManager = null;

        Destroy(currentLevel.gameObject);
        currentLevel = null;
    }


    public async void LoadNextLevel()
    {
        int nextIndex = currentLevelIndex + 1;
        if (nextIndex >= levels.Count) throw new Exception("No more levels available");
        await OnExit();
        LoadLevel(nextIndex);
    }

    public async void ReloadCurrentLevel()
    {
        await OnExit();
        LoadLevel(currentLevelIndex);
    }

    public async void ReturnToTitle()
    {
        await OnExit();
        SceneManager.LoadScene("Menu");
    }
}
