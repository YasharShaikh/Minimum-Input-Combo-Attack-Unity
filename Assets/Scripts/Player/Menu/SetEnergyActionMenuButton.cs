using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SetEnergyActionMenuButton : MonoBehaviour
{
    [SerializeField] EnergySO EnergyAttack;
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
            text.text = EnergyAttack.AttackName;

        if (image != null)
            image.sprite = EnergyAttack.UIImage;
    }

    public void OnButtonClick()
    {
        EnergySO OldAttack = PlayerCombatDynamic.instance.SwapEnergyAttack(EnergyAttack);
        EnergyAttack = OldAttack;
    }
}
