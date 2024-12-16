using UnityEngine;

public class ProjectileMagic : EnergyHandler
{
    private ObjectPool<Projectile> _projectilePool;

    public override void Execute(EnergySO energyData, Transform spawnPoint, Transform target = null)
    {
        if (energyData == null || energyData.energyPrefab == null)
        {
            Debug.LogError("Energy Data or Energy Prefab is missing!");
            return;
        }

        // Initialize the pool if it doesn't exist
        if (_projectilePool == null)
        {
            InitializePool(energyData);
        }

        // Get projectile from pool
        Projectile projectile = _projectilePool.Get(spawnPoint.position, Quaternion.identity);

       
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = projectile.gameObject.AddComponent<Rigidbody>();
            Debug.Log("Rigidbody added at runtime.");
        }




        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.useGravity = false;  // Disable gravity if not needed

        // Calculate direction and apply velocity
        Vector3 direction = target != null
            ? (target.position - spawnPoint.position).normalized
            : spawnPoint.forward;

        rb.linearVelocity = direction * energyData.projectileSpeed;

        // Setup projectile damage, effects, and pooling
        projectile.Setup(energyData, _projectilePool);
    }

    private void InitializePool(EnergySO energyData)
    {
        Projectile projectileScript = energyData.energyPrefab.GetComponent<Projectile>();
        if (projectileScript == null)
        {
            projectileScript = energyData.energyPrefab.AddComponent<Projectile>();
            Debug.LogWarning("Projectile script added at runtime.");
        }

        _projectilePool = new ObjectPool<Projectile>(
            projectileScript,
            2, transform
        );
    }
}
