using player;
using System;
using UnityEngine;
public class Sword : MonoBehaviour
{

    public event Action<Enemy> onEnemyHit;
    [SerializeField] float Damage;
    PlayerInputHandler inputHandler;
    Collider swordCollider;
    AttackSO attackSO;
    bool isAttacking = false;

    private void Awake()
    {
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        swordCollider = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
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
                enemy.TakeDamage(Damage);
                onEnemyHit?.Invoke(enemy);
                isAttacking = false;
                DisableSwordCollider();
            }
        }
    }

    private void EnableSwordCollider() => swordCollider.enabled = true;
    private void DisableSwordCollider() => swordCollider.enabled = false;
}
