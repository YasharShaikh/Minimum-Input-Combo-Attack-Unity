using UnityEngine;

public class ProjectileMagic : EnergyHandler
{
    public override void Execute(EnergySO energyData, Transform spawnPoint, Transform target = null)
    {
        if (energyData.energyPrefab == null) return;

        GameObject projectile = Instantiate(energyData.energyPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = target != null ? (target.position - spawnPoint.position).normalized : spawnPoint.forward;
            rb.linearVelocity = direction * energyData.projectileSpeed;
        }
        projectile.AddComponent<Projectile>().Setup(energyData);
    }
}
