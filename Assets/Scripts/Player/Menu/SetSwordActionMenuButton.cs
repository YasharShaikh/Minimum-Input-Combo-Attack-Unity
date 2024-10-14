using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetSwordActionMenuButton : MonoBehaviour
{
    [SerializeField] private AttackSO SwordAttack;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioSource as_actionMenu;
    [SerializeField] private AudioClip ac_actionMenuButton;

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

    public void OnButtonClick()
    {
        as_actionMenu.PlayOneShot(ac_actionMenuButton);
        AttackSO OldAttack = PlayerCombatDynamic.instance.SwapSwordAttack(SwordAttack);
        SwordAttack = OldAttack;
        UpdateButtonUI();
    }
}
