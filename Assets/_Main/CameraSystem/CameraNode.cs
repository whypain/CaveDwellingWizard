using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraNode : MonoBehaviour
{
    public CameraNode CamLeft => camLeft;
    public CameraNode CamRight => camRight;
    public CinemachineCamera Cam => thisCam;

    [SerializeField] CameraNode camLeft;
    [SerializeField] CameraNode camRight;

    private CinemachineCamera thisCam;

    private void Awake()
    {
        thisCam = GetComponent<CinemachineCamera>();
    }
}
