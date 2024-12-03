using UnityEngine;
using System;
using player;

public class Sword : MonoBehaviour
{
    public event Action<Enemy, float> onEnemyHit; // Pass enemy and damage
    [SerializeField] private float damage = 10f;

    PlayerInputHandler inputHandler;
    Collider swordCollider;
    bool isAttacking = false;

    private void Awake()
    {
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        swordCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        swordCollider.enabled = false;
    }

    private void Update()
    {
        if (inputHandler.swordATKTriggered)
        {
            isAttacking = true;
            EnableSwordCollider();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                onEnemyHit?.Invoke(enemy, damage); // Notify listeners with enemy and damage
                Time.timeScale = 0.1f;
                Invoke("Resume", 1f);
                isAttacking = false;
                DisableSwordCollider();
            }
        }
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    private void EnableSwordCollider() => swordCollider.enabled = true;
    private void DisableSwordCollider() => swordCollider.enabled = false;
}
