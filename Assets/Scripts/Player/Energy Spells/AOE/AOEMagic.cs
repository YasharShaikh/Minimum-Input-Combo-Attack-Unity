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
        if (_aoePool == null)
        {
            InitializePool(energyData);
        }
    }
    private void InitializePool(EnergySO energyData)
    {
        AOE aoe = energyData.GetComponent<AOE>();
        if (aoe == null)
        {
            aoe = energyData.energyPrefab.GetComponent<AOE>();
            Debug.LogWarning("AOE script added at runtime.");
        }
        _aoePool = new ObjectPool<AOE>(aoe, 2, transform);
    }
}
