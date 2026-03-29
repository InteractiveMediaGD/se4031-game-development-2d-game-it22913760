using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float rotationSpeed = 360f;

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}