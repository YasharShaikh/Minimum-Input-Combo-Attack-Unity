using UnityEngine;
using player;

public class ActionMenuHandler : MonoBehaviour
{
    [SerializeField] Canvas ActionMenuCanvas;

    PlayerInputHandler inputHandler;
    ActionMenuInputHandler actionMenuInputHandler;
    PlayerCombatHandler playerCombatHandler;


    private void Awake()
    {
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        actionMenuInputHandler = GetComponentInParent<ActionMenuInputHandler>();
        playerCombatHandler = GetComponentInParent<PlayerCombatHandler>();
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
            ActionMenuCanvas.gameObject.SetActive(true);
        }
        else
        {
            inputHandler.enabled = true;
            ActionMenuCanvas.gameObject.SetActive(false);

        }
    }
}
