using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float damageCooldown = 0.5f;
    public float knockbackForce = 3f;
    public float deathDelay = 1.2f;

    private bool canTakeDamage = true;
    private bool isDead = false;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerMovementTopDown movementScript;
    private PlayerShoot shootScript;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        movementScript = GetComponent<PlayerMovementTopDown>();
        shootScript = GetComponent<PlayerShoot>();

        if (GameManager.Instance != null)
            GameManager.Instance.UpdateHealthUI(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount, Vector2 damageSource, EnemyDamage attacker = null)
    {
        if (!canTakeDamage || isDead) return;

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (GameManager.Instance != null)
            GameManager.Instance.UpdateHealthUI(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die(attacker);
            return;
        }

        if (anim != null)
            anim.SetTrigger("Hurt");

        StartCoroutine(FlashRed());

        if (rb != null)
        {
            Vector2 knockDir = ((Vector2)transform.position - damageSource).normalized;
            rb.velocity = knockDir * knockbackForce;
        }

        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake(0.15f, 0.12f);

        StartCoroutine(DamageCooldownRoutine());
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (GameManager.Instance != null)
            GameManager.Instance.UpdateHealthUI(currentHealth, maxHealth);
    }

    private void Die(EnemyDamage attacker)
    {
        if (isDead) return;
        isDead = true;

        if (movementScript != null)
            movementScript.enabled = false;

        if (shootScript != null)
            shootScript.enabled = false;

        if (rb != null)
            rb.velocity = Vector2.zero;

        if (anim != null)
            anim.SetTrigger("Dead");

        if (attacker != null)
        {
            attacker.StartEating();
        }

        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);

        if (GameManager.Instance != null)
            GameManager.Instance.GameOver();
    }

    private IEnumerator FlashRed()
    {
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }

    private IEnumerator DamageCooldownRoutine()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}