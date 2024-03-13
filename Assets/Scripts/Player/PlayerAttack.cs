using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator playerAnimator;

    public enum PlayerAttackState
    {
        PAS_nmlSWD,
        PAS_nmlMAGIC,
        PAS_hvyMAGIC,
        PAS_hvySWD
    }

    public PlayerAttackState currentPAS;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponentInChildren<Animator>();
        currentPAS = PlayerAttackState.PAS_nmlSWD;
    }

    private void Update()
    {
        SwdATKButtonPressed();
        EngyATKButtonPressed();
    }

    private void SwdATKButtonPressed()
    {

        bool swdBTNpressed = playerInput.actions["swdATK"].triggered;
        if (swdBTNpressed)
        {
            Debug.Log("Performing sword attack");
            // Set trigger to transition to attack animation
            playerAnimator.SetTrigger("swdATK");
        }
    }

    private void EngyATKButtonPressed()
    {
        bool engyBTNpressed = playerInput.actions["pwrATK"].triggered;

        if (engyBTNpressed)
        {
            Debug.Log("Performing energy attack");
            // Set trigger to transition to energy attack animation
            playerAnimator.SetTrigger("engyATK");
        }
    }
}
