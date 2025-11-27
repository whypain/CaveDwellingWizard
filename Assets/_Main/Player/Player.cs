using PrimeTween;
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
    [SerializeField] Animator animator;

    [Header("Health")]
    [SerializeField] PlayerHealthVignette healthVignette;
    [SerializeField] Resource healthResource;

    private CollectiblesManager collectiblesManager;
    private Interactable currentInteractable;
    private PlayerController controller;
    private int slimeLeft;
    private bool isDead;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        slimeLeft = slimeCount;
    }


    void OnEnable()
    {
        InputSystem.actions["Player/Interact"].performed += OnInteract;
        InputSystem.actions["Player/Attack"].performed += OnShoot;

        healthResource.OnDepleted += OnDead;
    }

    void OnDisable()
    {
        InputSystem.actions["Player/Interact"].performed -= OnInteract;
        InputSystem.actions["Player/Attack"].performed -= OnShoot;

        healthResource.OnDepleted += OnDead;
    }


    public void Initialize()
    {
        collectiblesManager = new CollectiblesManager();
        interactableUI.HideImmediate();

        healthResource.Initialize();
        healthVignette.Initialize(healthResource);
    }

    public void SetInteractable(Interactable interactable)
    {
        currentInteractable = interactable;
    }

    public void PickupSlime()
    {
        slimeLeft++;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        healthResource.Decrease(amount);
        Debug.Log("Took damage");

        animator.SetTrigger("Hurt");
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

        SlimeSpawner bullet = Instantiate(slimeSpawnerPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = controller.GetFacingSide() * shootForce;

        // Bind to level object
        bullet.transform.SetParent(transform.parent);
        slimeLeft--;
    }

    void OnDead()
    {
        if (isDead) return;
        isDead = true;

        animator.SetTrigger("Dead");
        LevelManager.Instance.FailLevel();
    }
}