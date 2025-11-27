using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] Slime slimePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 collisionNormal = collision.contacts[0].normal;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, collisionNormal);
        Instantiate(slimePrefab, transform.position, rotation);
    }
}

public class Slime : MonoBehaviour
{

}
