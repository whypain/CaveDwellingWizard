using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleUI : MonoBehaviour
{
    public CollectibleSO Representing => collectible;

    [SerializeField] Image icon;
    [SerializeField] TMP_Text countText;

    private CollectibleSO collectible;
    private int count;

    public void Initialize(CollectibleSO collectible, int count)
    {
        this.collectible = collectible;
        icon.sprite = collectible.Icon;

        this.count = count;
        countText.text = count.ToString();
    }

    public void AddCount(int count)
    {
        if (collectible == null) return;

        this.count += count;
        countText.text = this.count.ToString();
    }
}
