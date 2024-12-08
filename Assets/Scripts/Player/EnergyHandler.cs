using UnityEngine;

public abstract class EnergyHandler : MonoBehaviour
{
    public abstract void Execute(EnergySO energyData, Transform spawnPoint, Transform target = null);
}
