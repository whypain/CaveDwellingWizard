using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class SaveLoadSystem
{
    public static void ClearSave()
    {
        PlayerPrefs.DeleteKey(Constants.SAVED_LEVEL);
        PlayerPrefs.DeleteKey(Constants.PLAYER_DATA);
    }

    public static async Task Save(string levelName, PlayerData playerData, int camNode)
    {
        PlayerPrefs.SetString(Constants.SAVED_LEVEL, levelName);
        PlayerPrefs.SetString(Constants.PLAYER_DATA, playerData.Pack());
        PlayerPrefs.SetInt(Constants.CAMERA_NODE, camNode);
        
        await Task.Delay(100); // Wait a frame to ensure save is complete
    }


    public static async Task<(Level, PlayerData, int)> Load()
    {
        Level level;
        PlayerData playerData;
        int camNode;

        if (PlayerPrefs.HasKey(Constants.PLAYER_DATA))
        {
            string levelAddress = PlayerPrefs.GetString(Constants.SAVED_LEVEL);
            AsyncOperationHandle<GameObject> operation = Addressables.LoadAssetAsync<GameObject>(levelAddress);
            GameObject loaded = await operation.Task;
            level = loaded.GetComponent<Level>();
            loaded.name = levelAddress;

            string raw = PlayerPrefs.GetString(Constants.PLAYER_DATA);
            playerData = PlayerData.Unpack(raw);

            camNode = PlayerPrefs.GetInt("CameraNode", 0);
        }
        else
        {
            AsyncOperationHandle<GameObject> operation = Addressables.LoadAssetAsync<GameObject>(Constants.FIRST_LEVEL);
            GameObject loaded = await operation.Task;
            level = loaded.GetComponent<Level>();
            playerData = new PlayerData();
            camNode = 0;
        }

        return (level, playerData, camNode);
    }
}

public static class Constants
{
    public static string SAVED_LEVEL => "SavedLevel";
    public static string PLAYER_DATA => "PlayerData";
    public static string FIRST_LEVEL => "Tutorial";
    public static string CAMERA_NODE => "CameraNode";
}
