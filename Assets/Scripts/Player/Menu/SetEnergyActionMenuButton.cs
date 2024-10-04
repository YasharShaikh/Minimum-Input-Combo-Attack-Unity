using UnityEngine;

public class SetEnergyActionMenuButton : MonoBehaviour
{
    [SerializeField] EnergySO EnergyAttack;

    public void OnButtonClick()
    {
        EnergySO OldAttack = PlayerCombatDynamic.instance.SwapEnergyAttack(EnergyAttack);
        EnergyAttack = OldAttack;
    }
}
