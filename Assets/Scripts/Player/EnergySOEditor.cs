#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnergySO))]
public class EnergySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Reference to the target ScriptableObject
        EnergySO energySO = (EnergySO)target;

        EditorGUILayout.LabelField("General Info", EditorStyles.boldLabel);
        energySO.AttackName = EditorGUILayout.TextField("Attack Name", energySO.AttackName);
        energySO.UIImage = (Sprite)EditorGUILayout.ObjectField("UI Image", energySO.UIImage, typeof(Sprite), false);
        energySO.controller = (AnimatorOverrideController)EditorGUILayout.ObjectField("Animation Controller", energySO.controller, typeof(AnimatorOverrideController), false);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage and Effects", EditorStyles.boldLabel);
        energySO.damage = EditorGUILayout.Slider("Damage", energySO.damage, 0, 100);
        energySO.ps_Explosion = (ParticleSystem)EditorGUILayout.ObjectField("Explosion Effect", energySO.ps_Explosion, typeof(ParticleSystem), false);
        energySO.groundKnockBack = EditorGUILayout.Slider("Ground Knockback", energySO.groundKnockBack, 0f, 10f);
        energySO.forwardStep = EditorGUILayout.Slider("Forward Step", energySO.forwardStep, 0f, 10f);
        energySO.lifeTime = EditorGUILayout.Slider("Life Time", energySO.lifeTime, 0.5f, 10f);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Attack Settings", EditorStyles.boldLabel);
        energySO.energyPrefab = (GameObject)EditorGUILayout.ObjectField("Energy Prefab", energySO.energyPrefab, typeof(GameObject), false);
        energySO.energyType = (EnergyType)EditorGUILayout.EnumPopup("Energy Type", energySO.energyType);
        energySO.stunDuration = EditorGUILayout.Slider("Stun Duration", energySO.stunDuration, 0f, 5f);
        energySO.projectileSpeed = EditorGUILayout.Slider("Projectile Speed", energySO.projectileSpeed, 0f, 50f);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(energySO);
        }
    }
}
#endif
