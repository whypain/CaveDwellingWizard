using System.Collections.Generic;
using UnityEngine;

public class CollectibleUIManager : MonoBehaviour
{
    [SerializeField] CollectibleUI collectibleUI_Prefab;
    [SerializeField] Transform uiContainer;

    private CollectiblesManager collectiblesManager;
    private Dictionary<CollectibleSO, CollectibleUI> activeUIs = new Dictionary<CollectibleSO, CollectibleUI>();

    public void Initialize(CollectiblesManager manager)
    {
        foreach (Transform child in uiContainer)
        {
            Destroy(child.gameObject);
        }

        collectiblesManager = manager;
        collectiblesManager.OnCollected += OnCollected;
        activeUIs.Clear();
    }

    private void OnCollected(CollectibleSO collectible, int count)
    {
        if (activeUIs.TryGetValue(collectible, out var ui))
        {
            ui.AddCount(count);
            return;
        }

        var collectibleUI = Instantiate(collectibleUI_Prefab, uiContainer);
        collectibleUI.Initialize(collectible, count);
        activeUIs[collectible] = collectibleUI;
    }
}
