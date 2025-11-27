using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] Slime prefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _)) return;

        Vector3 collisionNormal = collision.contacts[0].normal;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, collisionNormal);
        // rotation.eulerAngles = rotation.eulerAngles.With(z: rotation.eulerAngles.z - 90f);
        Instantiate(prefab, transform.position, rotation);
        Destroy(gameObject);
    }
}
