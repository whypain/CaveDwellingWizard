using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerData Data => playerData;
    public InteractableUI InteractableUI => interactableUI;

    [SerializeField] InteractableUI interactableUI;

    private PlayerData playerData;
    private Interactable currentInteractable;


    void OnEnable()
    {
        InputSystem.actions["Player/Interact"].performed += OnInteract;
    }

    void OnDisable()
    {
        InputSystem.actions["Player/Interact"].performed -= OnInteract;
    }


    public void Initialize(PlayerData playerData)
    {
        this.playerData = playerData;
        interactableUI.HideImmediate();
    }

    public void SetInteractable(Interactable interactable)
    {
        currentInteractable = interactable;
    }

    public void OnCompleteLevel()
    {
        Debug.Log("Player has completed the level!");
    }


    void OnInteract(InputAction.CallbackContext _)
    {
        currentInteractable?.Interact(this);
    }
}

public class PlayerData
{
    public int Milk => milk;
    public int Time => time;
    public int Cookies => cookies;
    
    private int milk;
    private int cookies;
    private int time;

    public PlayerData(int milk, int cookies, int time, Vector2 position)
    {
        this.milk = milk;
        this.cookies = cookies;
        this.time = time;
    }

    public PlayerData()
    {
        milk = 0;
        cookies = 0;
        time = 0;
    }
}
