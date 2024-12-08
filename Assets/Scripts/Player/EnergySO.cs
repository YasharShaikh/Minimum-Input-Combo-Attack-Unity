using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Attacks/Energy Attack")]
public class EnergySO : ScriptableObject
{
    public string AttackName;
    public Sprite UIImage;
    public AnimatorOverrideController controller;
    public float damage;
    public float groundKnockBack;
    public float forwardStep;
    public GameObject energyPrefab;
    public EnergyType energyType;
    public float stunDuration;
    public float projectileSpeed;
}
public enum EnergyType
{
    Projectile,
    Stun,
    Tornado
}