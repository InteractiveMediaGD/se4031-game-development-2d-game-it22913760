using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    private Vector3 offset;

    private void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        desired.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}