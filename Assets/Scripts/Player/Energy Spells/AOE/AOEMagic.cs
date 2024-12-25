using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class AOEMagic : EnergyHandler
{
    private ObjectPool<AOE> _aoePool;

    public override void Execute(EnergySO energyData, Transform spawnPoint, Transform target = null)
    {
        if (energyData == null || energyData.energyPrefab == null)
        {
            Debug.LogError("Energy Data or Energy Prefab is missing!");
            return;
        }

        // Initialize the pool if it doesn't exist
        if (_aoePool == null)
        {
            InitializePool(energyData);
        }

        // Get an AOE object from the pool and set it up
        AOE aoe = _aoePool.Get(spawnPoint.position, Quaternion.identity);
        aoe.Setup(energyData, _aoePool);
    }

    private void InitializePool(EnergySO energyData)
    {
        if (_aoePool == null)
        {
            AOE aoePrefab = energyData.energyPrefab.GetComponent<AOE>();
            if (aoePrefab == null)
            {
                Debug.LogError("AOE Prefab Missing Component!");
                return;
            }

            _aoePool = new ObjectPool<AOE>(aoePrefab, 5, transform);
            Debug.Log("AOE Pool Initialized.");
        }
    }
}
