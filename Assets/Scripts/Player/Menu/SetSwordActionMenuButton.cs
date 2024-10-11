using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetSwordActionMenuButton : MonoBehaviour
{
    [SerializeField] private AttackSO SwordAttack;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;


    private void OnEnable()
    {
        UpdateButtonUI();
    }

    // This method updates the button's text and image
    private void UpdateButtonUI()
    {
        if (text != null)
            text.text = SwordAttack.AttackName;

        if (image != null)
            image.sprite = SwordAttack.UIImage;
    }

    public void OnButtonClickSword()
    {
        AttackSO OldAttack = PlayerCombatDynamic.instance.SwapSwordAttack(SwordAttack);
        SwordAttack = OldAttack;
        UpdateButtonUI(); // Refresh the button UI with the new sword attack data
    }


}
