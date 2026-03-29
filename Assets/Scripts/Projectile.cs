using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f;
    public GameObject hitEffectPrefab;

    private Vector2 direction;
    private float speed;

    public void SetDirection(Vector2 dir, float moveSpeed)
    {
        direction = dir.normalized;
        speed = moveSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void SpawnHitEffect(Vector3 pos)
    {
        if (hitEffectPrefab != null)
            Instantiate(hitEffectPrefab, pos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(1, direction);
            SpawnHitEffect(transform.position);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Trap") || other.CompareTag("Wall"))
        {
            SpawnHitEffect(transform.position);
            Destroy(gameObject);
        }
    }
}