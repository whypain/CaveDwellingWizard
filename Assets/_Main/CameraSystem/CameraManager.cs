using System.Threading.Tasks;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera camPrefab;
    [SerializeField] float crossingThreshold = 0.1f;
    [SerializeField] float switchCamCooldown = 1f;
    [SerializeField] AnimationCurve transitionCurve;
    [SerializeField] float transitionDuration;

    private Player player;
    private Vector3 vs_lastPlayerPosition; // Prefix vs_ means Viewport Space

    private CinemachineCamera mainCam;
    private Camera cam;

    private bool isOnCooldown;
    private float camDistance;

    private Vector3 k_startingCamPos => new Vector3(0, 0, -10);

    public void Initialize(Player player, int camNode)
    {
        this.player = player;
        if (player == null) throw new System.Exception("Player can not be null.");

        mainCam = Instantiate(camPrefab, transform);

        mainCam.transform.localPosition = k_startingCamPos;
        cam = Camera.main;

        // Sample point to the left to calculate camera distance
        Vector3 samplePos = cam.ViewportToWorldPoint(new Vector3(-0.5f, 0.5f, 0f));
        camDistance = Vector2.Distance(samplePos, mainCam.transform.position);

        // Move the starting camera along the x-axis based on the saved camNode
        mainCam.transform.position += camNode * camDistance * new Vector3(1, 0, 0);
    }

    void Update()
    {
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
        MoveCamera(MoveDirection.Left);
    }

    private void OnPlayerExitRight()
    {
        MoveCamera(MoveDirection.Right);
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
        await Task.Delay((int)(switchCamCooldown * 1000));
        isOnCooldown = false;
    }
}

public static class Vector3Extension
{
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(
            x ?? original.x,
            y ?? original.y,
            z ?? original.z
        );
    }
}

public enum MoveDirection
{
    Left,
    Right
}
