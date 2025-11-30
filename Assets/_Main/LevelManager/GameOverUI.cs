using System;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button returnToTitle;
    [SerializeField] Button retryLevel;
    [SerializeField] Button nextLevel;

    [SerializeField] TMP_Text headerText;
    [SerializeField] TMP_Text timeTakenText;
    [SerializeField] Transform collectiblesContainer;
    [SerializeField] CollectibleUI collectibleUI_Prefab;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 0.5f;

    public void Initialize(CollectiblesManager collectibleManager, string header, float timeTaken)
    {
        gameObject.SetActive(true);
        headerText.text = header;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeTaken);
        timeTakenText.text = string.Format("{0:D2}:{1:D2}.{2:D3}",
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds
        );

        foreach (Transform child in collectiblesContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var kvp in collectibleManager.Collectibles)
        {
            var collectibleUI = Instantiate(collectibleUI_Prefab, collectiblesContainer);
            collectibleUI.Initialize(kvp.Key, kvp.Value);
        }

        nextLevel.gameObject.SetActive(false);

        Tween.Alpha(canvasGroup, 0f, 1f, fadeDuration);
    }

    public void InitNextLevelButton()
    {
        nextLevel.gameObject.SetActive(true);
        nextLevel.onClick.RemoveAllListeners();
        nextLevel.onClick.AddListener(OnNextLevel);
    }

    void OnNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}