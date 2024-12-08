using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthComponent healthComponent;
    AnimationComponent animationComponent;
    SoundComponent soundComponent;
    EffectComponent effectComponent;

    Sword playerSword;
    public bool isAlive=>healthComponent.IsAlive;
    private void Awake()
    {
        playerSword = FindAnyObjectByType<Sword>();
        healthComponent = GetComponent<HealthComponent>();
        animationComponent = GetComponent<AnimationComponent>();
        soundComponent = GetComponent<SoundComponent>();
        effectComponent = GetComponent<EffectComponent>();
    }

    private void OnEnable()
    {
        if (playerSword != null)
        {
            playerSword.onEnemyHit += HandleEnemyHit;
        }
    }

    private void OnDisable()
    {
        if (playerSword != null)
        {
            playerSword.onEnemyHit -= HandleEnemyHit;
        }
    }

    private void HandleEnemyHit(Enemy targetEnemy, float damage)
    {
        if (targetEnemy == this)
        {
            TakeDamage(damage);
        }
    }
    public void Stun(float stunDuration)
    {
        StartCoroutine(Stunned(stunDuration));
    }
    IEnumerator Stunned(float duration)
    {
        Debug.Log("Stunned");
        yield return new WaitForSeconds(duration);  
    }
    public void TakeDamage(float damage)
    {
        healthComponent.ReduceHealth(damage);

        if (healthComponent.IsAlive)
        {
            animationComponent.PlayHitAnimation();
            soundComponent.PlayHitSound();
            effectComponent?.PlayHitParticle();
        }
        else
        {
            animationComponent.PlayDeathAnimation();
            soundComponent.PlayDeathSound();
        }
    }
}
