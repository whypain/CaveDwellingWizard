public class InteractivePickup : InteractiveArea
{
    private IPickupable owner;

    public override void InternalInteract(Player player)
    {
        owner.OnPickup(player);
    }

    internal void Initialize(IPickupable pickupable)
    {
        owner = pickupable;
    }
}