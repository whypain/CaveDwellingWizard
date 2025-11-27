using UnityEngine;

public class Goal : InteractiveArea
{
    public override void InternalInteract(Player _)
    {
        LevelManager.Instance.EndLevel();
        Debug.Log("Goal reached!");
    }
}
