using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public int ammoAmount = 24;
    public float lifeTime = 6f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerShoot shoot = other.GetComponent<PlayerShoot>();
        if (shoot == null)
            shoot = other.GetComponentInParent<PlayerShoot>();

        if (shoot != null)
        {
            shoot.AddReserveAmmo(ammoAmount);
        }

        Destroy(gameObject);
    }
}