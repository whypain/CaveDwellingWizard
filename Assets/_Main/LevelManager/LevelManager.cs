using UnityEditor;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] CameraManager camManager;
    [SerializeField] Player player;

    public Level CurrentLevel => currentLevel;
    public Player Player => currentPlayer;

    private SaveLoadSystem saveLoadSystem = new SaveLoadSystem();
    private Player currentPlayer;
    private Level currentLevel;

    private Vector3 k_camPos => new Vector3(0, -1, 0);


    [ContextMenu("Test Load Level")]
    private void TestLoadLevel()
    {
        if (!EditorApplication.isPlaying) throw new System.Exception("Must be in play mode");
        LoadLevel();
    }

    [ContextMenu("Test Save Level")]
    private void TestSaveLevel()
    {
        if (!EditorApplication.isPlaying) throw new System.Exception("Must be in play mode");
        saveLoadSystem.Save(CurrentLevel.name, Player.Data);
    }

    [ContextMenu("Clear Save")]
    private void ClearSave()
    {
        saveLoadSystem.ClearSave();
    }

    [ContextMenu("Test Save & Exit")]
    private void TestSaveAndExitLevel()
    {
        if (!EditorApplication.isPlaying) throw new System.Exception("Must be in play mode");
        saveLoadSystem.Save(CurrentLevel.name, Player.Data);
        OnExit();
    }

    private void OnExit()
    {
        Destroy(currentLevel.gameObject);
        currentLevel = null;
    }

    public async void LoadLevel()
    {
        if (currentLevel != null) throw new System.Exception("There is already a level loaded");
        if (player == null || camManager == null) throw new System.Exception("Player or CamManager is null");

        (Level level, PlayerData playerData) = await saveLoadSystem.Load();

        Level spawnedLevel = Instantiate(level, transform);
        Player spawnedPlayer = Instantiate(player, spawnedLevel.PlayerSpawnPoint);
        CameraManager spawnedCam = Instantiate(camManager, spawnedLevel.PlayerSpawnPoint);

        spawnedLevel.name = level.name;
        spawnedLevel.transform.position = Vector3.zero;

        spawnedPlayer.transform.localPosition = playerData.Position;
        spawnedCam.transform.localPosition = k_camPos;

        spawnedPlayer.Initialize(playerData);
        spawnedCam.Initialize(spawnedPlayer);

        currentLevel = spawnedLevel;
        currentPlayer = spawnedPlayer;
    }
}
