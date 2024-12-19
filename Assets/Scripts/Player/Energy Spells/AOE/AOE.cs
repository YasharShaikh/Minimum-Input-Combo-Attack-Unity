using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem ps_MeteorRain;
    [SerializeField] private AudioSource as_MeteorRain;
    private ObjectPool<AOE> pool;

    private float damage;
    private List<ParticleSystem.Particle> hitParticles = new List<ParticleSystem.Particle>();

    private void Awake()
    {
        EnsureParticleSystem();
        EnsureAudioSource();
    }

    public void Setup(EnergySO energyData, ObjectPool<AOE> pool)
    {
        damage = energyData.damage;
        this.pool = pool;

        // Setup trigger module
        var trigger = ps_MeteorRain.trigger;
        trigger.enabled = true;
        trigger.enter = ParticleSystemOverlapAction.Callback;

        PlayMeteorSound();
    }

    private void OnEnable()
    {
        //Invoke(nameof(ReturnToPool), ps_MeteorRain.main.duration);
    }

    // ** Correctly Triggered Automatically by Unity **
    private void OnParticleTrigger()
    {
        int numCollisions = ps_MeteorRain.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, hitParticles);

        if (numCollisions == 0) return; // No collisions detected

        for (int i = 0; i < numCollisions; i++)
        {
            ParticleSystem.Particle particle = hitParticles[i];
            Collider[] hitColliders = Physics.OverlapSphere(particle.position, 0.5f);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    Enemy enemy = hitCollider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        Debug.Log($"Enemy Hit: {hitCollider.name}");
                    }
                }
            }
        }
    }

    private void ReturnToPool()
    {
        ps_MeteorRain.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        pool.ReturnToPool(this);
        Debug.Log("Returned AOE to Pool.");
    }

    private void EnsureParticleSystem()
    {
        if (ps_MeteorRain == null)
        {
            ps_MeteorRain = GetComponent<ParticleSystem>();
            if (ps_MeteorRain == null)
            {
                Debug.LogError("Particle System Missing!");
            }
        }
    }

    private void EnsureAudioSource()
    {
        if (as_MeteorRain == null)
        {
            as_MeteorRain = gameObject.AddComponent<AudioSource>();
            as_MeteorRain.spatialBlend = 1.0f;
            as_MeteorRain.rolloffMode = AudioRolloffMode.Linear;
            as_MeteorRain.maxDistance = 50.0f;
        }
    }

    private void PlayMeteorSound()
    {
        if (!as_MeteorRain.isPlaying)
        {
            as_MeteorRain.pitch = Random.Range(1.0f, 1.5f);
            as_MeteorRain.loop = true;
            as_MeteorRain.Play();
        }
    }
}
