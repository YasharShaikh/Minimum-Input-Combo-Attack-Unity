using UnityEngine;
using player;
using UnityEngine.Events;

public class ActionMenuHandler : MonoBehaviour
{
    [SerializeField] private Canvas ActionMenuCanvas;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private ActionMenuInputHandler actionMenuInputHandler;

    [SerializeField] private UnityEvent onMenuOpened;
    [SerializeField] private UnityEvent onMenuClosed;

    public bool IsActionMenuOpen { get; private set; }

    private void Awake()
    {
        ValidateDependencies();
        ActionMenuCanvas.gameObject.SetActive(false);
        IsActionMenuOpen = false;
    }

    private void Update()
    {
        HandleActionMenu();
    }

    private void HandleActionMenu()
    {
        if (actionMenuInputHandler.RadialMenuTriggered && !IsActionMenuOpen)
        {
            ToggleActionMenu(true);
        }
        else if (!actionMenuInputHandler.RadialMenuTriggered && IsActionMenuOpen)
        {
            ToggleActionMenu(false);
        }
    }

    private void ToggleActionMenu(bool isOpen)
    {
        if (ActionMenuCanvas == null) return;

        IsActionMenuOpen = isOpen;
        inputHandler.enabled = !isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = isOpen;
        ActionMenuCanvas.gameObject.SetActive(isOpen);

        if (isOpen)
            onMenuOpened?.Invoke();
        else
            onMenuClosed?.Invoke();
    }

    private void ValidateDependencies()
    {
#if UNITY_EDITOR
        if (ActionMenuCanvas == null)
            Debug.LogError("ActionMenuCanvas is not assigned.");
        if (inputHandler == null)
            Debug.LogError("PlayerInputHandler is not assigned.");
        if (actionMenuInputHandler == null)
            Debug.LogError("ActionMenuInputHandler is not assigned.");
#endif
    }
}
