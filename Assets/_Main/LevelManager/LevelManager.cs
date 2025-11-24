using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Levels")]
    [SerializeField] List<Level> levels;

    [Header("References")]
    [SerializeField] CameraManager camManager;
    [SerializeField] Player player;
    [SerializeField] LevelUI ui;

    public Level CurrentLevel => currentLevel;
    private Level currentLevel;


    private void Start()
    {
        LoadLevel(0);
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
        if (player == null || camManager == null) throw new System.Exception("Player or CamManager is null");

        Level spawnedLevel = Instantiate(levels[index], transform);
        spawnedLevel.name = levels[index].name;
        spawnedLevel.transform.position = Vector3.zero;
        currentLevel = spawnedLevel;
        ui.FadeIn(3);
    }
}
