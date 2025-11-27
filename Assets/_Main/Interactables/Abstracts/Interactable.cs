using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool CanInteract(Player player);

    /// <summary>
    /// What happens when the player interacts with this interactable.
    /// </summary>
    /// <param name="player"></param>
    public abstract void InternalInteract(Player player);

    public void Interact(Player player)
    {
        InternalInteract(player);
    }
}
