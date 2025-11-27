using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public CollectiblesManager CollectibleManager => collectiblesManager;
    public InteractableUI InteractableUI => interactableUI;
    public PlayerController Controller => controller;

    [SerializeField] InteractableUI interactableUI;
    [SerializeField] SlimeSpawner slimeSpawnerPrefab;
    [SerializeField] int slimeCount = 2;
    [SerializeField] float shootForce = 30f;

    private CollectiblesManager collectiblesManager;
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


    public void Initialize()
    {
        collectiblesManager = new CollectiblesManager();
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
