using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour
{
    public int damage = 15;
    public float damageCooldown = 1f;

    private bool canDamage = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!canDamage) return;
        if (!other.CompareTag("Player")) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health == null)
            health = other.GetComponentInParent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage, transform.position);
            StartCoroutine(DamageCooldownRoutine());
        }
    }

    private IEnumerator DamageCooldownRoutine()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}