using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Energy Attack")]
public class EnergySO : ScriptableObject
{
    [Header("General Info")]
    public string AttackName;
    [Tooltip("The icon displayed in the UI")]
    public Sprite UIImage;
    [Tooltip("Animator override controller for specific attack animations")]
    public AnimatorOverrideController controller;

    [Header("Damage and Effects")]
    [Range(0, 100)]
    [Tooltip("Base damage dealt by the energy attack")]
    public float damage;

    [Tooltip("Explosion effect triggered on impact")]
    public ParticleSystem ps_Explosion;

    [Range(0f, 10f)]
    [Tooltip("How far the target is pushed back on the ground")]
    public float groundKnockBack;

    [Range(0f, 10f)]
    [Tooltip("How far the caster moves forward when attacking")]
    public float forwardStep;

    [Tooltip("How long the projectile exists before being destroyed")]
    [Range(0.5f, 10f)]
    public float lifeTime;

    [Tooltip("Sound Effect to be played when Fired")]
    public AudioClip ac_Fire;

    [Tooltip("Sound Effect to be played when it hits the target")]
    public AudioClip ac_Hit;


    [Header("Attack Settings")]
    [Tooltip("Prefab for the projectile or energy effect")]
    public GameObject energyPrefab;

    [Tooltip("Type of energy effect")]
    public EnergyType energyType;

    [Tooltip("Duration of the stun effect")]
    [Range(0f, 5f)]
    public float stunDuration;

    [Tooltip("Speed of the projectile if applicable")]
    [Range(0f, 50f)]
    public float projectileSpeed;
}

public enum EnergyType
{
    Projectile,
    Stun,
    Tornado
}
