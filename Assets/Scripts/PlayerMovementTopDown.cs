using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 7f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        bool isMoving = movement.sqrMagnitude > 0.01f;
        bool isRunning = isMoving && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);
            anim.SetBool("isRunning", isRunning);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * currentSpeed;
    }
}