using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f;

    private bool canDamage = true;
    private ZombieAnimationController zombieAnim;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        zombieAnim = GetComponent<ZombieAnimationController>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!canDamage) return;
        if (!other.CompareTag("Player")) return;
        if (enemyHealth != null && enemyHealth.IsDead) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            if (zombieAnim != null)
            {
                zombieAnim.SetMoving(false);
                zombieAnim.PlayAttack();
            }

            playerHealth.TakeDamage(damage, transform.position, this);
            StartCoroutine(DamageCooldownRoutine());
        }
    }

    public void StartEating()
    {
        if (zombieAnim != null)
            zombieAnim.StartEating();
    }

    private IEnumerator DamageCooldownRoutine()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}