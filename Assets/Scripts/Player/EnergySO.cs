using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Energy Attack")]
public class EnergySO : ScriptableObject
{
   
    public string AttackName;
    public Sprite UIImage;
    public AnimatorOverrideController controller;

    [Range(0, 100)]
    public float damage;

    public ParticleSystem ps_Explosion;

    [Range(0f, 10f)]
    public float groundKnockBack;

    [Range(0f, 10f)]
    public float forwardStep;

    [Range(0.5f, 10f)]
    public float lifeTime;

    public AudioClip ac_Fire;

    public AudioClip ac_Hit;


    public GameObject energyPrefab;

    public EnergyType energyType;

    [Range(0f, 5f)]
    public float stunDuration;

    [Range(0f, 50f)]
    public float projectileSpeed;
}

public enum EnergyType
{
    Projectile,
    Stun,
    Tornado,
    AOE
}
