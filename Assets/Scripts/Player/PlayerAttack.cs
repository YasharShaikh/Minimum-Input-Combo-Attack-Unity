using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{


    public PlayerAttackState currentPAS;

    public enum PlayerAttackState
    {
        PAS_nmlSWD,
        PAS_nmlMAGIC,
        PAS_hvyMAGIC,
        PAS_hvySWD

    }


    private void Awake()
    {
        currentPAS = PlayerAttackState.PAS_nmlSWD;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        SwdATKButtonPressed();
        EngyATKButtonPressed();
    }

    // Update is called once per frame
    private void SwdATKButtonPressed()
    {
        bool swdBTNpressed = PlayerStateManager.Instance.playerInput.actions["swordAttack"].triggered;

        if (swdBTNpressed)
        {
            Debug.Log("Performing sword attack");
            PlayerStateManager.Instance.SwdAttack = true;
        }
    }

    private void EngyATKButtonPressed()
    {
        bool engyBTNpressed = PlayerStateManager.Instance.playerInput.actions["energyAttack"].triggered;

        if (engyBTNpressed)
        {
            Debug.Log("Performing energy attack");
            PlayerStateManager.Instance.EngyAttack = true;
        }
    }

}
