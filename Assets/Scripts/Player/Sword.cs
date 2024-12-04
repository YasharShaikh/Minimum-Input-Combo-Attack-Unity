using UnityEngine;
using System;
using System.Collections;
using player;

public class Sword : MonoBehaviour
{
    public event Action<Enemy, float> onEnemyHit; // Pass enemy and damage
    [SerializeField] private float damage = 10f;

    PlayerInputHandler inputHandler;
    Collider swordCollider;
    bool isAttacking = false;
    private bool isSlowMotionActive = false;

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
                onEnemyHit?.Invoke(enemy, damage);
                isAttacking = false;
                StartCoroutine(TriggerSlowMotion(0.1f, 0.8f)); // Slow motion for 1 second
                DisableSwordCollider();
            }
        }
    }

    private IEnumerator TriggerSlowMotion(float duration, float slowTimeScale)
    {
        if (isSlowMotionActive) yield break;

        isSlowMotionActive = true;
        Time.timeScale = slowTimeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        isSlowMotionActive = false;
    }

    private void EnableSwordCollider() => swordCollider.enabled = true;
    private void DisableSwordCollider() => swordCollider.enabled = false;
}
