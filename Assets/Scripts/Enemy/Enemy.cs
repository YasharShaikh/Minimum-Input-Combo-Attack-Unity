using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthComponent healthComponent;
    AnimationComponent animationComponent;
    SoundComponent soundComponent;
    EffectComponent effectComponent;
    

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        animationComponent = GetComponent<AnimationComponent>();
        soundComponent = GetComponent<SoundComponent>();
        effectComponent = GetComponent<EffectComponent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        healthComponent.ReduceHealth(damage);

        if (healthComponent.IsAlive)
        {
            animationComponent.PlayHitAnimation();
            soundComponent.PlayHitSound();
            //effectComponent.PlayHitEffect();
        }
        else
        {
            animationComponent.PlayDeathAnimation();
            soundComponent.PlayDeathSound();
        }
    }

}
