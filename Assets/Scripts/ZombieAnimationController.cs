using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private Animator animator;

    private bool isDead = false;
    private bool isEating = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // 🔹 Movement (Idle ↔ Walk)
    public void SetMoving(bool moving)
    {
        if (animator == null || isDead || isEating) return;

        animator.SetBool("IsMoving", moving);
    }

    // 🔹 Attack animation
    public void PlayAttack()
    {
        if (animator == null || isDead || isEating) return;

        animator.SetTrigger("Attack");
    }

    // 🔹 Hurt animation
    public void PlayHurt()
    {
        if (animator == null || isDead) return;

        animator.SetTrigger("Hurt");
    }

    // 🔹 Death animation
    public void Die()
    {
        if (animator == null || isDead) return;

        isDead = true;

        animator.SetBool("IsMoving", false);
        animator.SetBool("IsDead", true);
    }

    // 🔹 Eating animation (after killing player)
    public void StartEating()
    {
        if (animator == null || isDead) return;

        isEating = true;

        animator.SetBool("IsMoving", false);
        animator.SetBool("IsEating", true);
    }

    // 🔹 Stop eating (optional)
    public void StopEating()
    {
        if (animator == null) return;

        isEating = false;
        animator.SetBool("IsEating", false);
    }

    // 🔹 Helpers (optional use)
    public bool IsDead()
    {
        return isDead;
    }

    public bool IsEating()
    {
        return isEating;
    }
}