using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Attacks/Sword Attack")]
public class AttackSO : ScriptableObject
{

    public string AttackName;
    public AnimatorOverrideController controller;
    public float damage;
}
