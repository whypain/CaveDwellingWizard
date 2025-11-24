using UnityEngine;

public class Goal : InteractiveArea
{
    public override void InternalInteract(Player player)
    {
        Debug.Log("Goal reached!");
        // LevelManager.Instance.EndLevel();
    }
}
