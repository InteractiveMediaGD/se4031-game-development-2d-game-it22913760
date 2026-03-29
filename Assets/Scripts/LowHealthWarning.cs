using UnityEngine;
using UnityEngine.UI;

public class LowHealthWarning : MonoBehaviour
{
    public static LowHealthWarning Instance;

    public Image overlay;
    public float maxAlpha = 0.4f;

    private void Awake()
    {
        Instance = this;
    }

    public void SetWarning(int currentHealth, int maxHealth)
    {
        if (overlay == null) return;

        float percent = (float)currentHealth / maxHealth;

        Color c = overlay.color;

        if (percent > 0.3f)
        {
            c.a = 0f;
        }
        else
        {
            float t = percent / 0.3f;
            c.a = Mathf.Lerp(maxAlpha, 0f, t);
        }

        overlay.color = c;
    }
}