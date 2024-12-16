using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float lifeTime;
    private ParticleSystem explosionEffect;
    private SphereCollider sphereCollider;
    private AudioSource as_Fire;
    private AudioClip ac_hit;
    private AudioClip ac_fire;
    private ObjectPool<Projectile> pool;

    private void Awake()
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.5f;  
        sphereCollider.isTrigger = true;
        as_Fire = gameObject.AddComponent<AudioSource>();
        SetupAudioSource();
    }
    public void Setup(EnergySO energyData, ObjectPool<Projectile> pool)
    {
        damage = energyData.damage;
        explosionEffect = energyData.ps_Explosion;  
        ac_hit = energyData.ac_Hit;
        ac_fire = energyData.ac_Fire;
        this.pool = pool;
        PlayFireSound();
    }

    private void PlayExplosiom()
    {
        if (explosionEffect != null)
        {
            var explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            PlayHitSound();
            explosion.Play();
            Invoke(nameof(ReturnToPool), explosion.main.duration);

        }
    }

    private void ReturnToPool()
    {
        sphereCollider.enabled = true;
        pool.ReturnToPool(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        sphereCollider.enabled = false;
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
        PlayExplosiom();
    }
    private void OnDestroy()
    {
        if (as_Fire.isPlaying)
        {
            as_Fire.Stop();  // Stop any lingering audio
        }
    }
    private void SetupAudioSource()
    {
        as_Fire.spatialBlend = 1.0f;  // 3D sound blending
        as_Fire.rolloffMode = AudioRolloffMode.Linear;
        as_Fire.maxDistance = 50f;
    }
    private void PlayFireSound()
    {
        float random = Random.Range(1f, 1.5f);
        as_Fire.pitch = random;
        as_Fire.clip = ac_fire;
        as_Fire.loop = true;
        if(as_Fire.isPlaying)
         as_Fire.Play();
    }
    private void PlayHitSound()
    {
        as_Fire.loop = false;
        if(as_Fire.isPlaying)
            as_Fire.Stop();
        as_Fire.PlayOneShot(ac_hit);
    }
}
