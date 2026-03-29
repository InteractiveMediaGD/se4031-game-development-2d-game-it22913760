using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int scoreOnDeath = 1;
    public bool IsDead { get; private set; }

    [Header("Knockback")]
    public float knockbackForce = 2.5f;

    private int currentHealth;
    private Collider2D col;
    private Rigidbody2D rb;
    private EnemyDamage enemyDamage;
    private ZombieAnimationController zombieAnim;

    private void Start()
    {
        currentHealth = maxHealth;
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemyDamage = GetComponent<EnemyDamage>();
        zombieAnim = GetComponent<ZombieAnimationController>();
    }

    public void TakeDamage(int amount, Vector2 hitDirection)
    {
        if (IsDead) return;

        currentHealth -= amount;

        if (zombieAnim != null)
            zombieAnim.PlayHurt();

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(hitDirection * knockbackForce, ForceMode2D.Impulse);
        }

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;

        if (zombieAnim != null)
            zombieAnim.Die();

        if (col != null)
            col.enabled = false;

        if (rb != null)
            rb.velocity = Vector2.zero;

        if (enemyDamage != null)
            enemyDamage.enabled = false;

        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(scoreOnDeath);

        StartCoroutine(DestroyAfterDeath());
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}