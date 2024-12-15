using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float lifeTime;
    private ParticleSystem explosionEffect;
    private SphereCollider collider;
    private AudioSource as_Fire;
    private AudioClip ac_hit;


    private void Awake()
    {
        collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 0.5f;  // Customize as needed
        collider.isTrigger = false;  // Ensure proper collision detection

        as_Fire = gameObject.AddComponent<AudioSource>();
    }
    public void Setup(EnergySO energyData)
    {
        damage = energyData.damage;
        explosionEffect = energyData.ps_Explosion;  // Assign explosion prefab reference
        ac_hit = energyData.ac_Hit;

        as_Fire.clip = energyData.ac_Fire;
        as_Fire.loop = true;
        as_Fire.Play();
    }


    private IEnumerator PlayExplosion()
    {
        if (explosionEffect != null)
        {
            var explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            as_Fire.loop = false;
            as_Fire.Stop();

            as_Fire.PlayOneShot(ac_hit);

            explosion.Play();

            yield return new WaitForSeconds(explosion.main.duration);

            Destroy(explosion.gameObject);  
            Destroy(gameObject);  
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        collider.enabled = false;
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        StartCoroutine(PlayExplosion()); // Destroy the projectile after impact
    }

    private void OnDestroy()
    {
        if (as_Fire.isPlaying)
        {
            as_Fire.Stop();  // Stop any lingering audio
        }
    }
}
