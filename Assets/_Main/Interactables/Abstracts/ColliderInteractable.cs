using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ColliderInteractable : Interactable
{
    [SerializeField] protected Collider2D interactionCollider;

    protected virtual void OnPlayerEnter(Player player) {}
    protected virtual void OnPlayerStay(Player player) {}
    protected virtual void OnPlayerExit(Player player) {}

    public override bool CanInteract(Player player)
    {
        return interactionCollider.bounds.Contains(player.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            OnPlayerEnter(player);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            OnPlayerStay(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            OnPlayerExit(player);
        }
    }

    private void OnValidate()
    {
        interactionCollider ??= GetComponent<Collider2D>();
    }
}