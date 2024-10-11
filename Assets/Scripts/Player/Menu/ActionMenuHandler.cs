using UnityEngine;
using player;

public class ActionMenuHandler : MonoBehaviour
{
    [SerializeField] Canvas ActionMenuCanvas;

    private PlayerInputHandler inputHandler;
    private ActionMenuInputHandler actionMenuInputHandler;
    private bool isActionMenuOpen;

    private void Awake()
    {
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        actionMenuInputHandler = GetComponentInParent<ActionMenuInputHandler>();
        ActionMenuCanvas.gameObject.SetActive(false);
        isActionMenuOpen = false;
    }

    void Update()
    {
        HandleActionMenu();
    }

    // Only enable or disable the action menu when the input changes
    private void HandleActionMenu()
    {
        if (actionMenuInputHandler.RadialMenuTriggered && !isActionMenuOpen)
        {
            OpenActionMenu();
        }
        else if (!actionMenuInputHandler.RadialMenuTriggered && isActionMenuOpen)
        {
            CloseActionMenu();
        }
    }

    private void OpenActionMenu()
    {
        isActionMenuOpen = true;
        inputHandler.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        ActionMenuCanvas.gameObject.SetActive(true);
        SoundManager.Instance.PlayActionMenuBG(true);
    }

    private void CloseActionMenu()
    {
        isActionMenuOpen = false;
        inputHandler.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ActionMenuCanvas.gameObject.SetActive(false);
        SoundManager.Instance.PlayActionMenuBG(false);
    }
}
