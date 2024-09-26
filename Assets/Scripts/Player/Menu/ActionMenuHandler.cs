using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;
using UnityEngine.Rendering;

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

    // Update is called once per frame
    void Update()
    {
        ShowActionMenuCanvas();
    }


    void ShowActionMenuCanvas()
    {
        if (inputHandler.swordRadialMenuTriggered)
        {
            ActionMenuCanvas.gameObject.SetActive(true);
            //playerCombatHandler.SwapSwordAttack()
        }
        else
            ActionMenuCanvas.gameObject.SetActive(false);
    }
}
