using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level CurrentLevel => currentLevel;

    [Header("Levels")]
    [SerializeField] List<Level> levels;

    [Header("References")]
    [SerializeField] LevelUI ui;

    private CameraManager camManager;
    private Player player;
    private Level currentLevel;


    private void Start()
    {
        LoadLevel(0);
        GetReferences();

        player.Initialize(new PlayerData());
        camManager.Initialize(player, 0);

        ui.FadeIn();
    }

    private void OnDestroy()
    {
        OnExit();
    }

    private void OnExit()
    {
        ui.FadeOut(onComplete: () => {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        });
    }

    public void LoadLevel(int index)
    {
        if (currentLevel != null) throw new System.Exception("There is already a level loaded");

        Level spawnedLevel = Instantiate(levels[index], transform);
        spawnedLevel.name = levels[index].name;
        spawnedLevel.transform.position = Vector3.zero;
        currentLevel = spawnedLevel;
    }

    private void GetReferences()
    {
        player ??= FindFirstObjectByType<Player>(FindObjectsInactive.Include) ?? throw new System.Exception("Player not found in scene");
        camManager ??= FindFirstObjectByType<CameraManager>(FindObjectsInactive.Include) ?? throw new System.Exception("CameraManager not found in scene");
        ui ??= FindFirstObjectByType<LevelUI>(FindObjectsInactive.Include) ?? throw new System.Exception("LevelUI not found in scene");
    }
}