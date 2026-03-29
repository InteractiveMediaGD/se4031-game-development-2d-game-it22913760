using UnityEngine;

public class ZombieIdleWobble : MonoBehaviour
{
    public float wobbleAmount = 0.03f;
    public float wobbleSpeed = 3f;

    private Vector3 startLocalPos;

    private void Start()
    {
        startLocalPos = transform.localPosition;
    }

    private void Update()
    {
        float y = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        transform.localPosition = startLocalPos + new Vector3(0f, y, 0f);
    }
}