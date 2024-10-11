using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Attacks/Energy Attack")]
public class EnergySO : ScriptableObject
{
    public string AttackName;
    public Sprite UIImage;
    public AnimatorOverrideController controller;
    public float damage;
    public float groundKnochBack;
    public float airKnockBack;
    public float forwardStep;
    public GameObject Energy;
}
