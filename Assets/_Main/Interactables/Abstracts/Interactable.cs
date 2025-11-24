using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool CanInteract(Player player);
    public abstract void InternalInteract(Player player);

    public void Interact(Player player)
    {
        InternalInteract(player);
    }
}
