using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;

    public void Setup(EnergySO energyData)
    {
        damage = energyData.damage;
        Collider collider = gameObject.AddComponent<Collider>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject); // Destroy the projectile after impact
    }
}
