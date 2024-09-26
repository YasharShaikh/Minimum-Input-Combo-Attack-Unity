using UnityEngine;
using player;

public class ActionMenuHandler : MonoBehaviour
{
    [SerializeField] Canvas ActionMenuCanvas;

    InputHandler inputHandler;
    PlayerCombatHandler playerCombatHandler;


    private void Awake()
    {
        inputHandler = GetComponentInParent<InputHandler>();
        playerCombatHandler = GetComponentInParent<PlayerCombatHandler>();
        ActionMenuCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        ShowActionMenuCanvas();
    }

    void ShowActionMenuCanvas()
    {
        if (inputHandler.RadialMenuTriggered)
            ActionMenuCanvas.gameObject.SetActive(true);
        else
            ActionMenuCanvas.gameObject.SetActive(false);
    }
}
