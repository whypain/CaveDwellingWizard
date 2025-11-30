# Cave Dwelling Wizard - UML Class Documentation

## Table of Contents
1. [Core Systems](#core-systems)
2. [Player System](#player-system)
3. [Interactables System](#interactables-system)
4. [Collectibles System](#collectibles-system)
5. [Audio System](#audio-system)
6. [Camera System](#camera-system)
7. [UI System](#ui-system)
8. [Level Management](#level-management)
9. [Resource System](#resource-system)
10. [Enemy System](#enemy-system)
11. [Utility Classes](#utility-classes)
12. [Class Relationships Diagram](#class-relationships-diagram)

---

## Core Systems

### **Singleton\<T\>** (Abstract Generic Class)
**Type:** Abstract base class for singleton pattern  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `+ static T Instance { get; private set; }` - Static instance reference

#### Methods:
- `- void Awake()` - Ensures singleton pattern
- `# virtual void AfterSingletonCheck()` - Hook for subclass initialization

#### Relationships:
- **Inherited by:** LevelManager, MenuSceneManager, SoundManager, BGMManager

---

## Player System

### **Player** (Class)
**Type:** Main player entity  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- PlayerController controller` - HAS-A PlayerController
- `- CollectiblesManager collectiblesManager` - HAS-A CollectiblesManager
- `- Interactable currentInteractable` - Reference to current interactable
- `- InteractableUI interactableUI` - HAS-A InteractableUI
- `- SlimeSpawner slimeSpawnerPrefab` - Reference to slime spawner prefab
- `- int slimeCount = 2` - Initial slime ammunition count
- `- float shootForce = 30f` - Force applied to projectiles
- `- Animator animator` - Reference to animator
- `- PlayerHealthVignette healthVignette` - HAS-A PlayerHealthVignette
- `- Resource healthResource` - HAS-A Resource for health
- `- AudioClip slimeShootClip` - Sound effect for shooting
- `- AudioClip playerHurtClip` - Sound effect for taking damage
- `- AudioClip playerDeadClip` - Sound effect for death
- `- int slimeLeft` - Current slime ammunition
- `- bool isDead` - Death state flag

#### Methods:
- `+ void Initialize()` - Initialize player systems
- `+ void SetInteractable(Interactable)` - Set current interactable object
- `+ void PickupSlime()` - Increase slime ammunition
- `+ void TakeDamage(int)` - Reduce health by amount
- `+ void OnCompleteLevel()` - Handle level completion
- `- void OnInteract(InputAction.CallbackContext)` - Input handler for interaction
- `- void OnShoot(InputAction.CallbackContext)` - Input handler for shooting
- `- void OnDead()` - Handle player death

#### Properties:
- `+ CollectiblesManager CollectibleManager { get; }`
- `+ InteractableUI InteractableUI { get; }`
- `+ PlayerController Controller { get; }`

#### Relationships:
- **HAS-A** PlayerController (composition)
- **HAS-A** CollectiblesManager (composition)
- **HAS-A** Resource (composition)
- **HAS-A** InteractableUI (aggregation)
- **HAS-A** PlayerHealthVignette (aggregation)
- **USES** Interactable (association)
- **USES** LevelManager (dependency)
- **USES** SoundManager (dependency)

---

### **PlayerController** (Class)
**Type:** Handles player movement and input  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- float jumpForce` - Jump force magnitude
- `- float falingGravity` - Gravity scale when falling
- `- float fallingThreshold` - Velocity threshold for falling state
- `- float speed` - Normal movement speed
- `- float dashSpeed` - Dash movement speed
- `- float dashDuration = 0.1f` - Duration of dash
- `- float dashCooldown = 1f` - Cooldown between dashes
- `- float minWalkingSpd = 0.1f` - Minimum velocity for walking animation
- `- float groundCheckDist = 0.1f` - Distance for ground check raycast
- `- LayerMask whatIsGround` - Layer mask for ground detection
- `- Transform playerTransform` - Player's transform
- `- Transform playerSprite` - Player sprite transform
- `- Animator animator` - Animator reference
- `- Rigidbody2D rb` - HAS-A Rigidbody2D
- `- bool hasMvmentInput` - Movement input state
- `- bool isGrounded` - Grounded state (computed property)
- `- bool isGroundedLastFrame` - Previous frame grounded state
- `- bool dashing` - Currently dashing flag
- `- bool canDash` - Can perform dash flag
- `- Vector2 mvmentInput` - Current movement input
- `- float currMvmentSpeed` - Current movement speed

#### Methods:
- `- void Start()` - Initialize controller
- `- void Update()` - Update animations and state
- `- void LateUpdate()` - Store previous frame state
- `- void FixedUpdate()` - Handle physics-based movement
- `- void Move()` - Apply movement force
- `- void OnMove(InputAction.CallbackContext)` - Movement input handler
- `- void UnMove(InputAction.CallbackContext)` - Movement release handler
- `- void OnJump(InputAction.CallbackContext)` - Jump input handler
- `- async void OnDash(InputAction.CallbackContext)` - Dash input handler
- `- void OnLanded()` - Handle landing state
- `- bool CheckGrounded()` - Check if player is grounded
- `+ void AddForce(Vector2, ForceMode2D)` - Add external force
- `+ Vector2 GetFacingSide()` - Get player facing direction

#### Relationships:
- **HAS-A** Rigidbody2D (composition)
- **HAS-A** Animator (aggregation)
- **HAS-A** Transform (aggregation)

---

## Interactables System

### **Interactable** (Abstract Class)
**Type:** Base class for all interactive objects  
**Inheritance:** IS-A MonoBehaviour

#### Methods:
- `+ abstract bool CanInteract(Player)` - Check if player can interact
- `+ abstract void InternalInteract(Player)` - Define interaction behavior
- `+ void Interact(Player)` - Execute interaction

#### Relationships:
- **Inherited by:** ColliderInteractable

---

### **ColliderInteractable** (Abstract Class)
**Type:** Base for collider-based interactions  
**Inheritance:** IS-A Interactable

#### Attributes:
- `# Collider2D interactionCollider` - HAS-A Collider2D

#### Methods:
- `# virtual void OnPlayerEnter(Player)` - Player enters trigger
- `# virtual void OnPlayerStay(Player)` - Player stays in trigger
- `# virtual void OnPlayerExit(Player)` - Player exits trigger
- `+ override bool CanInteract(Player)` - Check if player is in bounds
- `- void OnTriggerEnter2D(Collider2D)` - Unity trigger enter callback
- `- void OnTriggerStay2D(Collider2D)` - Unity trigger stay callback
- `- void OnTriggerExit2D(Collider2D)` - Unity trigger exit callback
- `- void OnValidate()` - Unity validation callback

#### Relationships:
- **IS-A** Interactable (inheritance)
- **HAS-A** Collider2D (composition)
- **Inherited by:** InteractiveArea, JumpPad, Hazard, Collectibles

---

### **InteractiveArea** (Abstract Class)
**Type:** Base for interactive trigger areas  
**Inheritance:** IS-A ColliderInteractable

#### Methods:
- `# override void OnPlayerEnter(Player)` - Show UI and set interactable
- `# override void OnPlayerExit(Player)` - Hide UI and clear interactable

#### Relationships:
- **IS-A** ColliderInteractable (inheritance)
- **Inherited by:** Goal, InteractivePickup

---

### **InteractivePickup** (Class)
**Type:** Handles pickup interactions  
**Inheritance:** IS-A InteractiveArea

#### Attributes:
- `- IPickupable owner` - USES IPickupable interface

#### Methods:
- `+ override void InternalInteract(Player)` - Trigger pickup
- `+ void Initialize(IPickupable)` - Set pickup owner

#### Relationships:
- **IS-A** InteractiveArea (inheritance)
- **USES** IPickupable (dependency)

---

### **JumpPad** (Class)
**Type:** Launches player upward  
**Inheritance:** IS-A ColliderInteractable

#### Attributes:
- `- float jumpForce = 50f` - Launch force
- `- AudioClip jumpPadClip` - Sound effect

#### Methods:
- `+ override void InternalInteract(Player)` - Apply jump force
- `# override void OnPlayerEnter(Player)` - Auto-trigger jump

#### Relationships:
- **IS-A** ColliderInteractable (inheritance)
- **USES** SoundManager (dependency)

---

### **Hazard** (Class)
**Type:** Damages player on contact  
**Inheritance:** IS-A ColliderInteractable

#### Attributes:
- `- float interactCooldown = 0.5f` - Damage cooldown
- `- int damageAmount = 10` - Damage per hit
- `- bool isOnCooldown = false` - Cooldown state
- `- float coolDownTimer = 0f` - Cooldown timer

#### Methods:
- `# override void OnPlayerEnter(Player)` - Damage on enter
- `# override void OnPlayerStay(Player)` - Damage while staying
- `+ override void InternalInteract(Player)` - Apply damage
- `- void Update()` - Update cooldown timer

#### Relationships:
- **IS-A** ColliderInteractable (inheritance)

---

### **Goal** (Class)
**Type:** Level completion trigger  
**Inheritance:** IS-A InteractiveArea

#### Methods:
- `+ override void InternalInteract(Player)` - Complete level

#### Relationships:
- **IS-A** InteractiveArea (inheritance)
- **USES** LevelManager (dependency)

---

### **IPickupable** (Interface)
**Type:** Interface for pickupable objects

#### Methods:
- `+ void OnPickup(Player)` - Define pickup behavior

#### Relationships:
- **Implemented by:** Slime

---

## Collectibles System

### **CollectiblesManager** (Class)
**Type:** Manages player's collected items  
**Inheritance:** Plain C# class (not MonoBehaviour)

#### Attributes:
- `+ readonly Dictionary<CollectibleSO, int> Collectibles` - Collection storage
- `+ Action<CollectibleSO, int> OnCollected` - Collection event

#### Methods:
- `+ CollectiblesManager()` - Constructor
- `+ void Collect(CollectibleSO, int)` - Add collectible
- `+ void Clear()` - Clear all collectibles

#### Relationships:
- **USES** CollectibleSO (association)
- **USED BY** Player (composition)
- **USED BY** CollectibleUIManager (association)

---

### **CollectibleSO** (ScriptableObject)
**Type:** Data definition for collectibles  
**Inheritance:** IS-A ScriptableObject

#### Attributes:
- `+ string Name` - Collectible name
- `+ Sprite Icon` - Collectible icon

#### Relationships:
- **USED BY** CollectiblesManager (association)
- **USED BY** Collectibles (association)
- **USED BY** CollectibleUI (association)

---

### **Collectibles** (Class)
**Type:** Collectible object in world  
**Inheritance:** IS-A ColliderInteractable

#### Attributes:
- `- CollectibleSO collectable` - USES CollectibleSO
- `- int quantity = 1` - Amount to collect

#### Methods:
- `+ override void InternalInteract(Player)` - Add to player collection
- `# override void OnPlayerEnter(Player)` - Auto-collect on touch

#### Relationships:
- **IS-A** ColliderInteractable (inheritance)
- **USES** CollectibleSO (association)

---

### **CollectibleUIManager** (Class)
**Type:** Manages collectible UI display  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- CollectibleUI collectibleUI_Prefab` - UI prefab reference
- `- Transform uiContainer` - UI parent transform
- `- CollectiblesManager collectiblesManager` - USES CollectiblesManager
- `- Dictionary<CollectibleSO, CollectibleUI> activeUIs` - Active UI elements

#### Methods:
- `+ void Initialize(CollectiblesManager)` - Setup UI manager
- `- void OnCollected(CollectibleSO, int)` - Handle collection event

#### Relationships:
- **USES** CollectiblesManager (association)
- **CREATES** CollectibleUI (aggregation)

---

### **CollectibleUI** (Class)
**Type:** Individual collectible UI element  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- Image icon` - Icon display
- `- TMP_Text countText` - Count display
- `- CollectibleSO collectible` - USES CollectibleSO
- `- int count` - Current count

#### Methods:
- `+ void Initialize(CollectibleSO, int)` - Setup UI element
- `+ void AddCount(int)` - Update count display

#### Properties:
- `+ CollectibleSO Representing { get; }`

#### Relationships:
- **USES** CollectibleSO (association)

---

## Audio System

### **SoundManager** (Class)
**Type:** Manages all audio playback  
**Inheritance:** IS-A Singleton\<SoundManager\>

#### Attributes:
- `- AudioSource bgmSource` - HAS-A AudioSource for background music
- `- AudioSource sfxSource` - HAS-A AudioSource for sound effects

#### Methods:
- `# override void AfterSingletonCheck()` - Make persistent across scenes
- `+ void PlayBGM(AudioClip, float)` - Play background music
- `+ void StopBGM()` - Stop background music
- `+ async void TransitionBGM(AudioClip, float, float)` - Fade between BGM tracks
- `+ void PlaySFX(AudioClip, float, Vector2?)` - Play sound effect
- `- void OnDestroy()` - Cleanup on destroy

#### Relationships:
- **IS-A** Singleton\<SoundManager\> (inheritance)
- **HAS-A** AudioSource (composition, 2 instances)
- **USED BY** BGMManager (dependency)
- **USED BY** Multiple game objects (dependency)

---

### **BGMManager** (Class)
**Type:** Manages background music transitions  
**Inheritance:** IS-A Singleton\<BGMManager\>

#### Attributes:
- `- AudioClip titleBGM` - Title screen music
- `- AudioClip levelBGM` - Gameplay music
- `- AudioClip levelFailedBGM` - Failure music
- `- AudioClip levelCompletedBGM` - Victory music

#### Methods:
- `- void Start()` - Play initial BGM
- `+ void PlayTitleBGM()` - Transition to title music
- `+ void PlayLevelBGM()` - Transition to level music
- `+ void PlayLevelFailedBGM()` - Transition to failure music
- `+ void PlayLevelCompletedBGM()` - Transition to victory music

#### Relationships:
- **IS-A** Singleton\<BGMManager\> (inheritance)
- **USES** SoundManager (dependency)

---

### **VolumeSettings** (Class)
**Type:** Manages audio mixer settings  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- AudioMixer audioMixer` - Reference to audio mixer
- `- Slider master` - Master volume slider
- `- Slider bgm` - BGM volume slider
- `- Slider sfx` - SFX volume slider

#### Methods:
- `- void Start()` - Setup sliders and load settings
- `- void OnDestroy()` - Save settings
- `- void OnMasterVolumeChanged(float)` - Update master volume
- `- void OnBGMVolumeChanged(float)` - Update BGM volume
- `- void OnSFXVolumeChanged(float)` - Update SFX volume
- `- void SaveSettings()` - Save to PlayerPrefs
- `- void LoadSettings()` - Load from PlayerPrefs

#### Relationships:
- **USES** AudioMixer (association)

---

## Camera System

### **CameraManager** (Class)
**Type:** Manages camera movement and transitions  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- CinemachineCamera camPrefab` - Camera prefab reference
- `- float crossingThreshold = 0.1f` - Screen edge threshold
- `- float switchCamCooldown = 1f` - Cooldown between moves
- `- AnimationCurve transitionCurve` - Movement animation curve
- `- float transitionDuration` - Duration of transition
- `- Camera cam` - Reference to main camera
- `- Player player` - USES Player
- `- Vector3 vs_lastPlayerPosition` - Player viewport position
- `- CinemachineCamera mainCam` - HAS-A CinemachineCamera
- `- bool isOnCooldown` - Cooldown state
- `- bool isInitialized` - Initialization state
- `- float camDistance` - Distance between camera positions

#### Methods:
- `- void Awake()` - Validate prefab
- `+ void Initialize(Player)` - Setup camera system
- `- void Update()` - Check player position for transitions
- `- void OnPlayerExitLeft()` - Handle left edge crossing
- `- void OnPlayerExitRight()` - Handle right edge crossing
- `- async void MoveCamera(MoveDirection)` - Animate camera movement
- `- async void CountCooldown()` - Handle cooldown timer
- `- void OnDrawGizmos()` - Draw camera boundaries in editor

#### Relationships:
- **USES** Player (association)
- **HAS-A** CinemachineCamera (composition)
- **USES** VectorExtension (utility)

---

### **CameraNode** (Class)
**Type:** Camera position node (legacy/unused?)  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- CameraNode camLeft` - Left neighbor camera
- `- CameraNode camRight` - Right neighbor camera
- `- CinemachineCamera thisCam` - HAS-A CinemachineCamera

#### Properties:
- `+ CameraNode CamLeft { get; }`
- `+ CameraNode CamRight { get; }`
- `+ CinemachineCamera Cam { get; }`

#### Methods:
- `- void Awake()` - Get CinemachineCamera component

#### Relationships:
- **HAS-A** CinemachineCamera (composition)
- **HAS-A** CameraNode references (aggregation)

---

### **MoveDirection** (Enum)
**Type:** Camera movement direction

#### Values:
- `Left`
- `Right`

---

## UI System

### **LevelUI** (Class)
**Type:** Handles level screen transitions  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- Image fadeImage` - Fade overlay image

#### Methods:
- `+ void FadeIn(float, Action)` - Fade from black (callback)
- `+ void FadeOut(float, Action)` - Fade to black (callback)
- `+ async Task FadeInAsync(float)` - Fade from black (async)
- `+ async Task FadeOutAsync(float)` - Fade to black (async)

#### Relationships:
- **USED BY** LevelManager (association)
- **USED BY** MenuSceneManager (association)

---

### **GameOverUI** (Class)
**Type:** Game over/level complete screen  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- Button returnToTitle` - Return button
- `- Button retryLevel` - Retry button
- `- Button nextLevel` - Next level button
- `- TMP_Text headerText` - Header text
- `- TMP_Text timeTakenText` - Time display
- `- Transform collectiblesContainer` - Collectibles container
- `- CollectibleUI collectibleUI_Prefab` - Collectible UI prefab
- `- CanvasGroup canvasGroup` - For fading
- `- float fadeDuration = 0.5f` - Fade duration

#### Methods:
- `+ void Initialize(CollectiblesManager, string, float)` - Setup game over screen
- `+ void InitNextLevelButton()` - Enable next level button
- `- void OnRetry()` - Handle retry button
- `- void OnReturnToTitle()` - Handle return to title
- `- void OnNextLevel()` - Handle next level button

#### Relationships:
- **USES** CollectiblesManager (association)
- **CREATES** CollectibleUI (aggregation)
- **USES** LevelManager (dependency)

---

### **InteractableUI** (Class)
**Type:** Shows interaction prompt  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- CanvasGroup canvasGroup` - For fading

#### Methods:
- `+ void Show()` - Fade in UI
- `+ void Hide()` - Fade out UI
- `+ void HideImmediate()` - Instantly hide UI

#### Relationships:
- **USED BY** Player (composition)
- **USED BY** InteractiveArea (dependency)

---

### **TimerUI** (Class)
**Type:** Displays level timer  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- TMP_Text timerText` - Timer display text
- `- Timer timer` - USES Timer

#### Methods:
- `+ void Initialize(Timer)` - Setup timer UI
- `- void OnEnable()` - Reset display
- `+ void OnDisable()` - Unsubscribe from events
- `+ void SetTime(float)` - Update time display

#### Relationships:
- **USES** Timer (association)

---

### **Settings** (Class)
**Type:** Settings menu controller  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- GameObject settingsMenu` - Settings panel
- `- List<GameObject> disableInMenu` - Objects to disable

#### Methods:
- `- void Start()` - Initialize settings
- `- void OnEnable()` - Subscribe to input
- `- void OnDisable()` - Unsubscribe from input
- `- void OnToggleSettings(InputAction.CallbackContext)` - Toggle settings menu

#### Relationships:
- **USES** LevelManager (dependency)

---

## Level Management

### **LevelManager** (Class)
**Type:** Manages level loading and state  
**Inheritance:** IS-A Singleton\<LevelManager\>

#### Attributes:
- `- List<GameObject> levels` - All level prefabs
- `- string levelCompleteText` - Completion message
- `- string levelFailedText` - Failure message
- `- LevelUI ui` - HAS-A LevelUI
- `- CollectibleUIManager collectibleUIManager` - HAS-A CollectibleUIManager
- `- Timer levelTimer` - HAS-A Timer
- `- TimerUI levelTimerUI` - HAS-A TimerUI
- `- GameOverUI gameOverUI` - HAS-A GameOverUI
- `- AudioClip levelCompleteClip` - Victory sound
- `- AudioClip levelFailedClip` - Failure sound
- `- CameraManager camManager` - Reference to camera manager
- `- Player player` - Reference to player
- `- GameObject currentLevel` - Current level instance
- `- int currentLevelIndex` - Current level index
- `- float timeTaken` - Completion time
- `- bool isLevelOver` - Level completion state
- `- bool isTransitioning` - Transition state

#### Properties:
- `+ GameObject CurrentLevel { get; }`
- `+ Player Player { get; }`
- `+ CameraManager CamManager { get; }`
- `+ bool IsLevelOver { get; }`
- `+ bool IsTransitioning { get; }`

#### Methods:
- `- void Start()` - Load first level
- `+ async void LoadLevel(int)` - Load level by index
- `- void InitializeSystems()` - Initialize level systems
- `+ void CompleteLevel()` - Handle level completion
- `+ void FailLevel()` - Handle level failure
- `- async Task OnExit()` - Cleanup and exit level
- `+ async void LoadNextLevel()` - Load next level
- `+ async void ReloadCurrentLevel()` - Reload current level
- `+ async void ReturnToTitle()` - Return to title screen
- `+ bool CanOpenSettings()` - Check if settings can be opened

#### Relationships:
- **IS-A** Singleton\<LevelManager\> (inheritance)
- **HAS-A** LevelUI (aggregation)
- **HAS-A** CollectibleUIManager (aggregation)
- **HAS-A** Timer (aggregation)
- **HAS-A** TimerUI (aggregation)
- **HAS-A** GameOverUI (aggregation)
- **MANAGES** Player (association)
- **MANAGES** CameraManager (association)
- **USES** BGMManager (dependency)
- **USES** SoundManager (dependency)

---

### **MenuSceneManager** (Class)
**Type:** Manages menu scene  
**Inheritance:** IS-A Singleton\<MenuSceneManager\>

#### Attributes:
- `- GameObject menuPanel` - Menu panel
- `- ParticleSystem menuParticleSystem` - Menu particles
- `- LevelUI ui` - HAS-A LevelUI

#### Methods:
- `- void Start()` - Initialize menu
- `+ void StartGame()` - Start game and load first level

#### Relationships:
- **IS-A** Singleton\<MenuSceneManager\> (inheritance)
- **HAS-A** LevelUI (aggregation)
- **USES** BGMManager (dependency)

---

## Resource System

### **Resource** (Class)
**Type:** Generic resource management (health, etc.)  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `+ Action<int, int> OnChanged` - Event: current, max values
- `+ Action<float> OnChanged01` - Event: normalized value
- `+ Action OnDecreased` - Event: value decreased
- `+ Action OnIncreased` - Event: value increased
- `+ Action OnDepleted` - Event: reached zero
- `+ Action OnRecovered` - Event: reached max
- `- int max = 100` - Maximum value
- `- int current` - Current value
- `# bool isInitialized` - Initialization state

#### Properties:
- `+ int Max { get; }`
- `+ int Current { get; }`
- `+ float Normalized { get; }`

#### Methods:
- `- void Awake()` - Initialize resource
- `# virtual void LateAwake()` - Hook for subclass initialization
- `+ virtual void Initialize()` - Enable resource
- `+ void Decrease(int)` - Decrease value
- `+ void Increase(int)` - Increase value
- `+ void Set(int)` - Set value directly

#### Relationships:
- **USED BY** Player (composition)
- **USED BY** PlayerHealthVignette (association)

---

### **PlayerHealthVignette** (Class)
**Type:** Visual health indicator  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- Volume volume` - Post-processing volume
- `- float min` - Minimum vignette intensity
- `- float max` - Maximum vignette intensity
- `- Vignette vignette` - Vignette effect
- `- Resource playerHealth` - USES Resource
- `- float current` - Current vignette value
- `- TweenSettings<float> tweenVignette` - Tween settings

#### Methods:
- `- void Awake()` - Setup vignette effect
- `+ void Initialize(Resource)` - Connect to health resource
- `- void UpdateVignette(float)` - Update visual effect

#### Relationships:
- **USES** Resource (association)

---

## Enemy System

### **Slime** (Class)
**Type:** Enemy/projectile entity  
**Inheritance:** IS-A MonoBehaviour  
**Implements:** IPickupable

#### Attributes:
- `- InteractivePickup pickupRange` - HAS-A InteractivePickup

#### Methods:
- `- void Awake()` - Initialize pickup range
- `+ void OnPickup(Player)` - Give slime to player

#### Relationships:
- **IMPLEMENTS** IPickupable (interface)
- **HAS-A** InteractivePickup (composition)

---

### **SlimeSpawner** (Class)
**Type:** Spawns slime on collision  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `- Slime prefab` - Slime prefab reference
- `- AudioClip spawnSound` - Spawn sound effect

#### Methods:
- `- void OnCollisionEnter2D(Collision2D)` - Spawn slime on collision

#### Relationships:
- **CREATES** Slime (factory pattern)
- **USES** SoundManager (dependency)

---

## Utility Classes

### **VectorExtension** (Static Class)
**Type:** Vector3 utility extension methods

#### Methods:
- `+ static Vector3 With(this Vector3, float?, float?, float?)` - Copy vector with selective component replacement

#### Relationships:
- **USED BY** Multiple classes (utility)

---

### **UtilButtons** (Class)
**Type:** Utility button handlers  
**Inheritance:** IS-A MonoBehaviour

#### Methods:
- `+ void QuitGame()` - Quit application
- `+ void ReturnToTitle()` - Return to title screen
- `+ void RestartLevel()` - Restart current level

#### Relationships:
- **USES** LevelManager (dependency)

---

### **Timer** (Class)
**Type:** Level timer  
**Inheritance:** IS-A MonoBehaviour

#### Attributes:
- `+ Action<float> OnTimerUpdate` - Timer update event
- `- float elapsedTime = 0f` - Elapsed time
- `- bool isRunning` - Running state

#### Methods:
- `+ void Initialize()` - Start timer
- `+ float Stop()` - Stop timer and return time
- `- void Update()` - Update timer

#### Relationships:
- **USED BY** LevelManager (association)
- **USED BY** TimerUI (association)

---

## Class Relationships Diagram

### Inheritance Hierarchy

```
MonoBehaviour
├── Singleton<T> (abstract)
│   ├── LevelManager
│   ├── MenuSceneManager
│   ├── SoundManager
│   └── BGMManager
├── Interactable (abstract)
│   └── ColliderInteractable (abstract)
│       ├── InteractiveArea (abstract)
│       │   ├── Goal
│       │   └── InteractivePickup
│       ├── JumpPad
│       ├── Hazard
│       └── Collectibles
├── Player
├── PlayerController
├── PlayerHealthVignette
├── Resource
├── Slime (implements IPickupable)
├── SlimeSpawner
├── CameraManager
├── CameraNode
├── LevelUI
├── GameOverUI
├── InteractableUI
├── TimerUI
├── CollectibleUI
├── CollectibleUIManager
├── Settings
├── VolumeSettings
├── UtilButtons
└── Timer

ScriptableObject
└── CollectibleSO

Interface
└── IPickupable
    └── implemented by Slime

Plain C# Classes
└── CollectiblesManager

Static Classes
└── VectorExtension

Enums
└── MoveDirection
```

### Key Composition Relationships (HAS-A)

- **Player** HAS-A:
  - PlayerController (required component)
  - CollectiblesManager
  - Resource (health)
  - InteractableUI
  - PlayerHealthVignette

- **LevelManager** HAS-A:
  - LevelUI
  - CollectibleUIManager
  - Timer
  - TimerUI
  - GameOverUI

- **SoundManager** HAS-A:
  - 2x AudioSource (BGM and SFX)

- **CameraManager** HAS-A:
  - CinemachineCamera

### Key Aggregation Relationships (USES)

- **Player** USES:
  - Interactable (current interaction target)
  - SlimeSpawner (instantiates)
  - LevelManager (dependency)
  - SoundManager (dependency)

- **CollectiblesManager** USES:
  - CollectibleSO (dictionary key)

- **LevelManager** MANAGES:
  - Player (finds and references)
  - CameraManager (finds and references)

### Key Dependency Relationships

- Most gameplay classes depend on:
  - **SoundManager** for audio playback
  - **LevelManager** for game state
  - **BGMManager** for music transitions

---

## Notes for UML Diagram Creation

### Recommended Diagram Sections

1. **Core Systems Diagram**: Singleton pattern and managers
2. **Player System Diagram**: Player, PlayerController, and related components
3. **Interactables Hierarchy Diagram**: Full inheritance tree
4. **Collectibles System Diagram**: Complete collectibles flow
5. **Audio System Diagram**: Sound management architecture
6. **UI System Diagram**: All UI components and relationships
7. **Camera System Diagram**: Camera management
8. **Complete System Overview**: High-level relationships

### Cardinality Notations

- Player `1` ---→ `1` PlayerController
- Player `1` ---→ `1` CollectiblesManager
- Player `1` ---→ `0..1` Interactable (current)
- LevelManager `1` ---→ `0..1` Player
- LevelManager `1` ---→ `*` GameObject (levels)
- CollectiblesManager `1` ---→ `*` CollectibleSO
- CollectibleUIManager `1` ---→ `*` CollectibleUI

### Key Design Patterns Used

1. **Singleton Pattern**: LevelManager, SoundManager, BGMManager, MenuSceneManager
2. **Observer Pattern**: Events (OnChanged, OnCollected, OnDepleted, etc.)
3. **Factory Pattern**: SlimeSpawner creates Slime instances
4. **Strategy Pattern**: Interactable hierarchy with polymorphic behavior
5. **Component Pattern**: Unity's component-based architecture

---

## Summary Statistics

- **Total Classes**: 36
- **Interfaces**: 1 (IPickupable)
- **Enums**: 1 (MoveDirection)
- **ScriptableObjects**: 1 (CollectibleSO)
- **Singletons**: 4
- **Abstract Classes**: 4
- **MonoBehaviour Classes**: 33
- **Plain C# Classes**: 1 (CollectiblesManager)
- **Static Classes**: 1 (VectorExtension)

---

*Generated: November 30, 2025*  
*Project: Cave Dwelling Wizard*  
*Documentation Format: UML-Ready Markdown*
