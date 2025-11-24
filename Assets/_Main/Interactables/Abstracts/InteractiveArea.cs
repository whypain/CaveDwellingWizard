public abstract class InteractiveArea : ColliderInteractable
{
    protected override void OnPlayerEnter(Player player)
    {
        player.SetInteractable(this);
        player.InteractableUI.Show();
    }

    protected override void OnPlayerExit(Player player)
    {
        player.SetInteractable(null);
        player.InteractableUI.Hide();
    }
}
