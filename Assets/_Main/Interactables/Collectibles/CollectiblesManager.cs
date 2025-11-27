using System;
using System.Collections.Generic;

public class CollectiblesManager
{
    public readonly Dictionary<CollectibleSO, int> Collectibles = new Dictionary<CollectibleSO, int>();
    public Action<CollectibleSO, int> OnCollected;

    public CollectiblesManager()
    {
        Collectibles = new Dictionary<CollectibleSO, int>();
    }

    public void Collect(CollectibleSO collectible, int amount)
    {
        if (Collectibles.ContainsKey(collectible))
        {
            Collectibles[collectible] += amount;
        }
        else
        {
            Collectibles[collectible] = amount;
        }

        OnCollected?.Invoke(collectible, amount);
    }

    public void Clear()
    {
        Collectibles.Clear();
    }
}
