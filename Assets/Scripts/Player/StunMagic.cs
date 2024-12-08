using UnityEngine;

public class StunMagic : EnergyHandler
{
    public override void Execute(EnergySO energyData, Transform spawnPoint, Transform target = null)
    {
        // Apply stun effect to nearby enemies
        Collider[] enemies = Physics.OverlapSphere(spawnPoint.position, energyData.groundKnockBack);
        foreach (var enemyCollider in enemies)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Stun(energyData.stunDuration);
                }
            }
        }

        // Visual effects
        if (energyData.energyPrefab != null)
        {
            Instantiate(energyData.energyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
