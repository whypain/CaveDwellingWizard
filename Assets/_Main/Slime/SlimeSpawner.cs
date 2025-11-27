using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] Slime prefab;
    [SerializeField] AudioClip spawnSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 collisionNormal = collision.contacts[0].normal;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, collisionNormal);
        Slime slime = Instantiate(prefab, transform.position, rotation);
        slime.transform.SetParent(transform.parent);

        SoundManager.Instance.PlaySFX(spawnSound, position: slime.transform.position);

        Destroy(gameObject);
    }
}
