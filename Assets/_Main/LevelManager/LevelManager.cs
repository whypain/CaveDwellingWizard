using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] CameraManager camManager;
    [SerializeField] Player player;

    public string CurrentLevelName => currentLevel.name;

    private SaveLoadSystem saveLoadSystem = new SaveLoadSystem();
    private Level currentLevel;

    private Vector3 k_camPos => new Vector3(0, -1, 0);


    [ContextMenu("Test Load Level")]
    private void TestLoadLevel()
    {
        if (!EditorApplication.isPlaying) throw new System.Exception("Must be in play mode");
        LoadLevel();
    }

    public async void LoadLevel()
    {
        if (player == null || camManager == null) throw new System.Exception("Player or CamManager is null");

        (Level level, PlayerData playerData) = await saveLoadSystem.Load();

        Level spawnedLevel = Instantiate(level, transform);
        Player spawnedPlayer = Instantiate(player, spawnedLevel.PlayerSpawnPoint);
        CameraManager spawnedCam = Instantiate(camManager, spawnedLevel.PlayerSpawnPoint);

        spawnedPlayer.transform.localPosition = playerData.Position;
        spawnedCam.transform.localPosition = k_camPos;

        spawnedPlayer.Initialize(playerData);
        spawnedCam.Initialize(spawnedPlayer);

        Assert.IsTrue((Vector2)spawnedPlayer.transform.localPosition == playerData.Position);
        Assert.IsTrue(spawnedCam.transform.localPosition == new Vector3(0, -1, 0));
    }
}
