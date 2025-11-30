using System;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera camPrefab;
    [SerializeField] float crossingThreshold = 0.1f;
    [SerializeField] float switchCamCooldown = 1f;
    [SerializeField] AnimationCurve transitionCurve;
    [SerializeField] float transitionDuration;
    [SerializeField] Camera cam;

    private Player player;
    private Vector3 vs_lastPlayerPosition; // Prefix vs_ means Viewport Space

    private CinemachineCamera mainCam;

    private bool isOnCooldown;
    private bool isInitialized;
    private float camDistance;

    private void Awake()
    {
        if (camPrefab == null) throw new Exception("Cam Prefab can not be null.");
    }

    public void Initialize(Player player)
    {
        this.player = player;

        if (player == null) throw new System.Exception("Player can not be null.");

        mainCam = Instantiate(camPrefab, transform);
        mainCam.transform.localPosition = new Vector3(0, 0, mainCam.transform.localPosition.z);
        Debug.Log($"CameraManager initialized with position: {mainCam.transform.localPosition}");
        
        // Sample point to the left to calculate camera distance
        Vector3 samplePos = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
        samplePos.y = mainCam.transform.position.y;
        samplePos.z = mainCam.transform.position.z;
        camDistance = Vector2.Distance(mainCam.transform.position, samplePos) * 2;
        PlayerPrefs.SetFloat("CamDistance", camDistance); // cache for debugging purposes

        mainCam.gameObject.SetActive(true);
        isInitialized = true;
        Debug.Log($"CameraManager initialized with position: {mainCam.transform.localPosition}");
    }

    void Update()
    {
        if (!isInitialized) return;

        if (player == null || cam == null) return;

        vs_lastPlayerPosition = cam.WorldToViewportPoint(player.transform.position);

        // For debugging purposes
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     Debug.Log($"mainCam.rect: {cam.rect} ss_lastPlayerPosition: {vs_lastPlayerPosition}");
        // }

        if (vs_lastPlayerPosition.x < crossingThreshold)
        {
            OnPlayerExitLeft();
        }
        else if (vs_lastPlayerPosition.x > cam.rect.width - crossingThreshold)
        {
            OnPlayerExitRight();
        }
    }

    private void OnPlayerExitLeft()
    {
        if (isOnCooldown) return;

        MoveCamera(MoveDirection.Left);
    }

    private void OnPlayerExitRight()
    {
        if (isOnCooldown) return;

        MoveCamera(MoveDirection.Right);
    }


    private async void MoveCamera(MoveDirection direction)
    {
        if (isOnCooldown) return;
        CountCooldown();

        Vector2 dir = direction == MoveDirection.Left ? Vector2.left : Vector2.right;
        Vector2 targetPos = mainCam.transform.localPosition + (Vector3)(dir * camDistance);
        float timeElapsed = 0f;

        while (Vector2.Distance(mainCam.transform.localPosition, targetPos) > 0.1f)
        {
            float newX = Mathf.Lerp(mainCam.transform.localPosition.x, targetPos.x, transitionCurve.Evaluate(timeElapsed/transitionDuration));
            mainCam.transform.localPosition = mainCam.transform.localPosition.With(x: newX);
            await Task.Yield();
            timeElapsed += Time.deltaTime;
        }

    }

    private async void CountCooldown()
    {
        isOnCooldown = true;
        await WebGLFriendly.Delay((int)(switchCamCooldown * 1000));
        isOnCooldown = false;
    }
    
    private void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        if (Camera.main != null)
        {
            Gizmos.color = Color.cyan;

            float camDistance = PlayerPrefs.GetFloat("CamDistance", 12f);
            for (int i = 0; i <= 10; i++)
            {
                Gizmos.DrawWireCube(transform.position + Vector3.left * camDistance * i, new Vector3(cam.orthographicSize * 2 * cam.aspect, cam.orthographicSize * 2, 1));
                Gizmos.DrawWireCube(transform.position + Vector3.right * camDistance * i, new Vector3(cam.orthographicSize * 2 * cam.aspect, cam.orthographicSize * 2, 1));
            }
        }
    }

    [ContextMenu("Test Move Camera Left")]
    private void TestMoveCameraLeft()
    {
        MoveCamera(MoveDirection.Left);
    }

    [ContextMenu("Test Move Camera Right")]
    private void TestMoveCameraRight()
    {
        MoveCamera(MoveDirection.Right);
    }
}

public enum MoveDirection
{
    Left,
    Right
}
