using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SaveLoadSystem 
{
    public void Save(string levelName, PlayerData playerData)
    {
        PlayerPrefs.SetString(Constants.SAVED_LEVEL, levelName);
        PlayerPrefs.SetString(Constants.PLAYER_DATA, $"{playerData.Milk}|{playerData.Cookies}|{playerData.Position}|{playerData.Time}");
    }


    public async Task<(Level, PlayerData)> Load()
    {
        Level level;
        PlayerData playerData;

        if (PlayerPrefs.HasKey(Constants.PLAYER_DATA))
        {
            string levelAddress = PlayerPrefs.GetString(Constants.SAVED_LEVEL);
            AsyncOperationHandle<GameObject> operation = Addressables.LoadAssetAsync<GameObject>(levelAddress);
            GameObject loaded = await operation.Task;
            level = loaded.GetComponent<Level>();

            string raw = PlayerPrefs.GetString(Constants.PLAYER_DATA);
            playerData = PlayerData.Unpack(raw);
        }
        else
        {
            AsyncOperationHandle<GameObject> operation = Addressables.LoadAssetAsync<GameObject>(Constants.FIRST_LEVEL);
            GameObject loaded = await operation.Task;
            level = loaded.GetComponent<Level>();
            playerData = new PlayerData();
        }

        return (level, playerData);
    }
}

public static class Constants
{
    public static string SAVED_LEVEL = "SavedLevel";
    public static string PLAYER_DATA = "PlayerData";
    public static string FIRST_LEVEL = "Tutorial";
}
