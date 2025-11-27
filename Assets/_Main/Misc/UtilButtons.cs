using UnityEngine;

public class UtilButtons : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ReturnToTitle()
    {
        LevelManager.Instance.ReturnToTitle();
    }

    public void RestartLevel()
    {
        LevelManager.Instance.ReloadCurrentLevel();
    }
}
