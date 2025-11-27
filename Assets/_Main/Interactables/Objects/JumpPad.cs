using UnityEngine;

public class JumpPad : ColliderInteractable
{
    [SerializeField] float jumpForce = 50f;

    public override void InternalInteract(Player player)
    {
        Debug.Log("JumpPad activated!");
        player.Controller.AddForce(transform.up * jumpForce);
    }

    protected override void OnPlayerEnter(Player player)
    {
        base.OnPlayerEnter(player);
        InternalInteract(player);
    }
}