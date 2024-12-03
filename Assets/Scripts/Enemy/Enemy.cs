using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthComponent healthComponent;
    AnimationComponent animationComponent;
    SoundComponent soundComponent;
    EffectComponent effectComponent;

    Sword playerSword;

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
        // Only respond if this enemy is the target
        if (targetEnemy == this)
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        healthComponent.ReduceHealth(damage);

        if (healthComponent.IsAlive)
        {
            animationComponent.PlayHitAnimation();
            soundComponent.PlayHitSound();
            //effectComponent?.PlayHitEffect();
        }
        else
        {
            animationComponent.PlayDeathAnimation();
            soundComponent.PlayDeathSound();
        }
    }
}
