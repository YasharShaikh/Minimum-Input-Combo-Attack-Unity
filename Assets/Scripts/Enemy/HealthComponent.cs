using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthSlider;
    private float currentHealth;

    public bool IsAlive => currentHealth > 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ReduceHealth(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        healthSlider.value = currentHealth / 100;
        Debug.Log($"Health: {currentHealth}/{maxHealth}");
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
