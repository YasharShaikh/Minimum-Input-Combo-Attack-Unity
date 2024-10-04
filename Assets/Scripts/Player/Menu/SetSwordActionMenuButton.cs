using UnityEngine;

public class SetSwordActionMenuButton : MonoBehaviour
{
    [SerializeField] AttackSO SwordAttack;
    
    public void OnButtonClick()
    {
       AttackSO OldAttack =  PlayerCombatDynamic.instance.SwapSwordAttack(SwordAttack);
        SwordAttack = OldAttack;
    }
}
