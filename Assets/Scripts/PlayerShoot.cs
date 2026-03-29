using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;
    public float fireCooldown = 0.2f;

    [Header("Ammo")]
    public int magazineSize = 12;
    public int currentAmmo;
    public int reserveAmmo = 60;
    public float reloadTime = 1.2f;
    private bool isReloading = false;

    [Header("UI")]
    public TMP_Text ammoText;
    public GunUIImage gunUI;

    [Header("Magazine Icon")]
    public Image magImage;
    public Sprite fullMagSprite;
    public Sprite halfMagSprite;
    public Sprite emptyMagSprite;

    [Header("Recoil")]
    public Transform body;
    public float recoilDistance = 0.06f;
    public float recoilTime = 0.05f;

    private float nextFireTime;
    private Animator anim;
    private Vector3 bodyStartLocalPos;
    private PlayerAudio playerAudio;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<PlayerAudio>();
        currentAmmo = magazineSize;

        if (body != null)
            bodyStartLocalPos = body.localPosition;
    }

    private void Start()
    {
        UpdateAmmoUI();
        UpdateMagIcon();
    }

    private void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            TryReload();
        }

        if (currentAmmo <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Magazine empty. Press R to reload.");
            }
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || shootPoint == null) return;
        if (currentAmmo <= 0) return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        Vector2 dir = (mouse - shootPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Projectile projectile = bullet.GetComponent<Projectile>();

        if (projectile != null)
            projectile.SetDirection(dir, bulletSpeed);

        currentAmmo--;
        UpdateAmmoUI();
        UpdateMagIcon();

        if (anim != null)
            anim.SetTrigger("Attack");

        if (gunUI != null)
            gunUI.ShowShootSprite();

        if (playerAudio != null)
        {
            playerAudio.PlayShootSound();
        }

        StartCoroutine(RecoilRoutine(dir));
    }

    public void TryReload()
    {
        if (isReloading) return;
        if (currentAmmo == magazineSize) return;
        if (reserveAmmo <= 0) return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        if (anim != null)
            anim.SetTrigger("Reload");

        if (playerAudio != null)
            playerAudio.PlayReloadSound();

        UpdateAmmoUI();

        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = magazineSize - currentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;
        UpdateAmmoUI();
        UpdateMagIcon();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            if (isReloading)
                ammoText.text = "Reloading...";
            else
                ammoText.text = currentAmmo + " / " + reserveAmmo;
        }
    }

    private void UpdateMagIcon()
    {
        if (magImage == null) return;

        if (currentAmmo <= 0)
        {
            magImage.sprite = emptyMagSprite;
        }
        else if (currentAmmo <= magazineSize / 2)
        {
            magImage.sprite = halfMagSprite;
        }
        else
        {
            magImage.sprite = fullMagSprite;
        }
    }

    public void AddReserveAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoUI();
    }

    private IEnumerator RecoilRoutine(Vector2 dir)
    {
        if (body == null) yield break;

        Vector3 recoilPos = bodyStartLocalPos - (Vector3)(dir * recoilDistance);
        body.localPosition = recoilPos;

        yield return new WaitForSeconds(recoilTime);

        body.localPosition = bodyStartLocalPos;
    }
}