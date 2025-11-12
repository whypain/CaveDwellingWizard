using System.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CameraNode startingCamera;
    [SerializeField] float crossingThreshold = 0.1f;
    [SerializeField] float switchCamCooldown = 1f;

    private Player player;
    private Vector3 vs_lastPlayerPosition; // Prefix vs_ means Viewport Space

    private CameraNode currentCamera;
    private Camera mainCam;

    private bool isOnCooldown;

    public void Initialize(Player player)
    {
        this.player = player;
        if (player == null) throw new System.Exception("Player can not be null.");

        mainCam = Camera.main;

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(child == mainCam.transform);
        }
        
        SwitchCamera(startingCamera);
    }

    void Update()
    {
        vs_lastPlayerPosition = mainCam.WorldToViewportPoint(player.transform.position);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"mainCam.rect: {mainCam.rect} ss_lastPlayerPosition: {vs_lastPlayerPosition}");
        }

        if (vs_lastPlayerPosition.x < crossingThreshold)
        {
            OnPlayerExitLeft();
        }
        else if (vs_lastPlayerPosition.x > mainCam.rect.width - crossingThreshold)
        {
            OnPlayerExitRight();
        }
    }

    private void SwitchCamera(CameraNode newCamera)
    {
        if (isOnCooldown) return;
        if (newCamera == null) return;
        if (currentCamera) currentCamera.Cam.gameObject.SetActive(false);

        currentCamera = newCamera;
        currentCamera.Cam.gameObject.SetActive(true);

        CountCooldown();
    }

    private void OnPlayerExitLeft()
    {
        SwitchCamera(currentCamera.CamLeft);
    }

    private void OnPlayerExitRight()
    {
        SwitchCamera(currentCamera.CamRight);
    }


    private async void CountCooldown()
    {
        isOnCooldown = true;
        await Task.Delay((int)(switchCamCooldown * 1000));
        isOnCooldown = false;
    }
}
