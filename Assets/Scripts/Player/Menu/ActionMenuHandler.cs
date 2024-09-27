using UnityEngine;
using player;

public class ActionMenuHandler : MonoBehaviour
{
    [SerializeField] Canvas ActionMenuCanvas;

    PlayerInputHandler inputHandler;
    ActionMenuInputHandler actionMenuInputHandler;


    private void Awake()
    {
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        actionMenuInputHandler = GetComponentInParent<ActionMenuInputHandler>();
        ActionMenuCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        ShowActionMenuCanvas();
    }

    void ShowActionMenuCanvas()
    {
        if (actionMenuInputHandler.RadialMenuTriggered)
        {
            inputHandler.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            ActionMenuCanvas.gameObject.SetActive(true);
        }
        else
        {
            inputHandler.enabled = true;
            Cursor.visible = false;
            ActionMenuCanvas.gameObject.SetActive(false);

        }
    }
}
