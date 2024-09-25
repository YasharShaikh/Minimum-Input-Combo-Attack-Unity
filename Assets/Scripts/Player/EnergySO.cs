using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Attacks/Energy Attack")]
public class EnergySO : ScriptableObject
{
    public string AttackName;
    public AnimatorOverrideController controller;
    public float damage;
}
