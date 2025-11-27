using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public PlayerData Data => playerData;
    public InteractableUI InteractableUI => interactableUI;
    public PlayerController Controller => controller;

    [SerializeField] InteractableUI interactableUI;
    [SerializeField] SlimeSpawner slimeSpawnerPrefab;
    [SerializeField] int slimeCount = 2;
    [SerializeField] float shootForce = 30f;

    private PlayerData playerData;
    private Interactable currentInteractable;
    private PlayerController controller;
    private int slimeLeft;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        slimeLeft = slimeCount;
    }


    void OnEnable()
    {
        InputSystem.actions["Player/Interact"].performed += OnInteract;
        InputSystem.actions["Player/Attack"].performed += OnShoot;
    }

    void OnDisable()
    {
        InputSystem.actions["Player/Interact"].performed -= OnInteract;
        InputSystem.actions["Player/Attack"].performed -= OnShoot;
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

    public void PickupSlime()
    {
        slimeLeft++;
    }

    public void OnCompleteLevel()
    {
        Debug.Log("Player has completed the level!");
    }


    void OnInteract(InputAction.CallbackContext _)
    {
        currentInteractable?.Interact(this);
    }

    void OnShoot(InputAction.CallbackContext _)
    {
        if (slimeLeft <= 0) return;

        var bullet = Instantiate(slimeSpawnerPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = controller.GetFacingSide() * shootForce;
        slimeLeft--;
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
