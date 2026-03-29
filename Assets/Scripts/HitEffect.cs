using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour
{
    public float lifeTime = 0.15f;
    public float maxScale = 1.4f;

    private void Start()
    {
        StartCoroutine(PopRoutine());
    }

    private IEnumerator PopRoutine()
    {
        Vector3 start = transform.localScale;
        Vector3 target = start * maxScale;

        float t = 0f;
        while (t < lifeTime)
        {
            t += Time.deltaTime / lifeTime;
            transform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }

        Destroy(gameObject);
    }
}