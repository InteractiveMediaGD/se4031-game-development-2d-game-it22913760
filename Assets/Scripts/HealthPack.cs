using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 25;
    public float lifeTime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(healAmount);
        }

        Destroy(gameObject);
    }
}