using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunUIImage : MonoBehaviour
{
    public Image gunImage;
    public Sprite idleGunSprite;
    public Sprite shootGunSprite;
    public float shootFlashTime = 0.08f;

    private Coroutine shootRoutine;

    private void Start()
    {
        if (gunImage != null && idleGunSprite != null)
            gunImage.sprite = idleGunSprite;
    }

    public void ShowShootSprite()
    {
        if (shootRoutine != null)
            StopCoroutine(shootRoutine);

        shootRoutine = StartCoroutine(ShootSpriteRoutine());
    }

    private IEnumerator ShootSpriteRoutine()
    {
        if (gunImage != null && shootGunSprite != null)
            gunImage.sprite = shootGunSprite;

        yield return new WaitForSeconds(shootFlashTime);

        if (gunImage != null && idleGunSprite != null)
            gunImage.sprite = idleGunSprite;
    }
}