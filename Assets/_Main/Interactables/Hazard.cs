using UnityEngine;

public class Hazard : ColliderInteractable
{
    [SerializeField] float interactCooldown = 0.5f;
    bool isOnCooldown = false;
    float coolDownTimer = 0f;

    protected override void OnPlayerEnter(Player player)
    {
        InternalInteract(player);
    }

    protected override void OnPlayerStay(Player player)
    {
        InternalInteract(player);
    }

    public override void InternalInteract(Player player)
    {
        if (isOnCooldown) return;

        Debug.Log("Player hit a hazard!");
        isOnCooldown = true;
        coolDownTimer = interactCooldown;
    }

    void Update()
    {
        if (isOnCooldown)
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }
}