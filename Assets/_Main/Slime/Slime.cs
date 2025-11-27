using UnityEngine;

public class Slime : MonoBehaviour, IPickupable
{
    [SerializeField] InteractivePickup pickupRange;

    private void Awake()
    {
        pickupRange.Initialize(this);
    }

    public void OnPickup(Player player)
    {
        player.PickupSlime();
        Destroy(gameObject);
    }
}
