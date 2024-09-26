using UnityEngine;

public class SetSwordActionMenuButton : MonoBehaviour
{
    [SerializeField] AttackSO SwordAttack;
    
    public void OnButtonClick()
    {
       AttackSO OldAttack =  PlayerCombatHandler.instance.SwapSwordAttack(SwordAttack);
        SwordAttack = OldAttack;
    }
}
