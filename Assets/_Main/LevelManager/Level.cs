using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform PlayerSpawnPoint => playerSpawnPoint;
    [SerializeField] Transform playerSpawnPoint;
}
