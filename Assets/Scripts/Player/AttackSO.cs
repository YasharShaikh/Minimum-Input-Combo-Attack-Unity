using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Attacks/Sword Attack")]
public class AttackSO : ScriptableObject
{
    public string AttackName;
    public AnimatorOverrideController controller;
    public float damage;
    public float attackSpeed;
    [Tooltip("How long until next attack")] public float recoil;
    public ParticleSystem SwingVFX;
    public ParticleSystem HitVFX;
}
