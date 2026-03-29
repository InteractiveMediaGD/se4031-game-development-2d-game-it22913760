using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stopDistance = 0.2f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyHealth enemyHealth;
    private ZombieAnimationController zombieAnim;

    private Transform body;
    private Vector3 originalBodyScale;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        zombieAnim = GetComponent<ZombieAnimationController>();

        if (anim != null)
        {
            body = anim.transform;
            originalBodyScale = body.localScale;
        }
    }

    private void FixedUpdate()
    {
        if (player == null || enemyHealth == null || enemyHealth.IsDead)
        {
            if (rb != null)
                rb.velocity = Vector2.zero;

            if (zombieAnim != null)
                zombieAnim.SetMoving(false);

            return;
        }

        Vector2 dir = (Vector2)player.position - rb.position;
        float dist = dir.magnitude;

        if (dist > stopDistance)
        {
            dir = dir.normalized;
            float finalSpeed = moveSpeed;

            if (GameManager.Instance != null)
                finalSpeed *= GameManager.Instance.zombieSpeedMultiplier;

            rb.velocity = dir * finalSpeed;

            Debug.Log("MOVING");

            if (zombieAnim != null)
                zombieAnim.SetMoving(true);
        }
        else
        {
            rb.velocity = Vector2.zero;

            Debug.Log("STOPPED");

            if (zombieAnim != null)
                zombieAnim.SetMoving(false);
        }

        if (body != null)
        {
            Vector3 scale = originalBodyScale;

            if (player.position.x < transform.position.x)
                scale.x = -Mathf.Abs(originalBodyScale.x);
            else
                scale.x = Mathf.Abs(originalBodyScale.x);

            body.localScale = scale;
        }
    }
}