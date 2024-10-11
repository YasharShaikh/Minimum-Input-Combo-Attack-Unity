using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(menuName = "Attacks/Sword Attack")]
public class AttackSO : ScriptableObject
{
    public string AttackName;
    public Sprite UIImage;
    public AnimatorOverrideController controller;
    public float damage;
    public float attackSpeed;
    public float forwardStep;
    public float KnockBackForce;
    [Tooltip("How long until next attack")] public float recoil;
    public GameObject SwingVFX;
    public ParticleSystem HitVFX;
}
