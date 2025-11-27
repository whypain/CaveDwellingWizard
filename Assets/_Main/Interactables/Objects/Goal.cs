using UnityEngine;

public class Goal : InteractiveArea
{
    public override void InternalInteract(Player _)
    {
        LevelManager.Instance.CompleteLevel();
        Debug.Log("Goal reached!");
    }
}
