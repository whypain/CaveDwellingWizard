using UnityEngine;

public class Collectibles : ColliderInteractable
{
    [SerializeField] CollectibleSO collectable;
    [SerializeField] int quantity = 1;

    public override void InternalInteract(Player player)
    {
        player.CollectibleManager.Collect(collectable, quantity);
    }

    protected override void OnPlayerEnter(Player player)
    {
        base.OnPlayerEnter(player);
        player.CollectibleManager.Collect(collectable, quantity);
        Destroy(gameObject);
    }
}
