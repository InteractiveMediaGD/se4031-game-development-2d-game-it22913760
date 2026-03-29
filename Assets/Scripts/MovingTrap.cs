using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;
    public float moveRangeX = 3f;
    public float moveRangeY = 3f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;

    private void Start()
    {
        startPosition = transform.position;
        PickNewTarget();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (timer >= changeDirectionTime || Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            timer = 0f;
            PickNewTarget();
        }
    }

    private void PickNewTarget()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);

        targetPosition = startPosition + new Vector3(randomX, randomY, 0f);
    }
}