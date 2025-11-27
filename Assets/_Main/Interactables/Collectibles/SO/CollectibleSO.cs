using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible", menuName = "Collectible")]
public class CollectibleSO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
}