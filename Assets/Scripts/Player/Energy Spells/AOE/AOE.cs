using System.Collections.Generic;
using UnityEngine;


public class AOE : MonoBehaviour
{
    private ParticleSystem ps_MeteorRain;
    private AudioSource as_MeteorRain;
    private ObjectPool<AOE> pool;
    private float damage;
    private List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
    private List<Enemy> hitEnemies = new List<Enemy>();

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                hitEnemies.Add(enemy);
                RegisterEnemies(other);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && hitEnemies.Contains(enemy))
            {
                hitEnemies.Remove(enemy);
                DeregisterEnemies(other);
            }
        }
    }

    private void RegisterEnemies(Collider col)
    {
        var trigger = ps_MeteorRain.trigger;
        trigger.enabled = true;
        trigger.enter = ParticleSystemOverlapAction.Callback;
        trigger.SetCollider(trigger.colliderCount, col);
    }
    private void DeregisterEnemies(Collider col)
    {
        var trigger = ps_MeteorRain.trigger;

        for (int i = 0; i < trigger.colliderCount; i++)
        {
            if (trigger.GetCollider(i) == col)
            {
                for (int j = i; j < trigger.colliderCount - 1; j++)
                {
                    trigger.SetCollider(j, trigger.GetCollider(j + 1));
                }
                trigger.SetCollider(trigger.colliderCount - 1, null);
                return;
            }
        }
    }

    private void OnParticleTrigger()
    {
        int numEnter = ps_MeteorRain.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

        if (numEnter == 0) return;

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle particle = enterParticles[i];
            Collider[] hitColliders = Physics.OverlapSphere(particle.position, 0.5f);

            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    Enemy enemy = col.GetComponent<Enemy>();
                    if (enemy != null)
                        enemy.TakeDamage(damage);
                }
            }
        }

        // Reset the trigger particles to clear the processed ones
        ps_MeteorRain.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
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
