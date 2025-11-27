using UnityEngine;

public class JumpPad : ColliderInteractable
{
    [SerializeField] float jumpForce = 50f;
    [SerializeField] AudioClip jumpPadClip;

    public override void InternalInteract(Player player)
    {
        player.Controller.AddForce(transform.up * jumpForce);
        SoundManager.Instance.PlaySFX(jumpPadClip);
    }

    protected override void OnPlayerEnter(Player player)
    {
        base.OnPlayerEnter(player);
        InternalInteract(player);
    }
}