using player;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    public static PlayerCombatHandler instance;

    [SerializeField] float timeBetweenCombo;
    [SerializeField] int mainAtkComboStream;
    [SerializeField] int maxCombo = 3;
    [SerializeField] float lastClickTime;

    [Header("Avaible Sword Attacks")]
    [SerializeField] List<AttackSO> swordAttacks = new List<AttackSO>();
    [SerializeField] List<EnergySO> energyAttacks = new List<EnergySO>();

    bool isPerformingCombo;
    PlayerAnimationHandler playerAnimationHandler;
    PlayerInputHandler inputHandler;



    private void Awake()
    {
        instance = this;

        playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainAtkComboStream = 0;
        lastClickTime = 0f;
        isPerformingCombo = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isPerformingCombo)
        {
            lastClickTime = 0;
        }
        else
        {
            lastClickTime += Time.deltaTime;
        }

        // Check if the player has triggered a sword or power attack
        if ((inputHandler.swordATKTriggered || inputHandler.powerATKTriggered) && (!playerAnimationHandler.isPerformingSwordAttack && !playerAnimationHandler.isPerformingEnergyAttack))
        {
            isPerformingCombo = true;
            // If the current main attack combo stream is zero, start a new combo
            if (mainAtkComboStream == 0 || mainAtkComboStream == maxCombo)
            {
                Debug.Log("Before StartCombo");
                StartCombo();
            }
            else if (mainAtkComboStream > 0 && mainAtkComboStream < maxCombo)
            {
                // Check if the time between combo attacks has elapsed
                if (lastClickTime < timeBetweenCombo)
                {
                    //Continue the combo attack
                    ContinueCombo();
                }
                else
                {
                    Debug.Log("reset combo due to timebetweencombo");
                    // Start a new combo attack
                    StartCombo();
                }
            }
        }
    }

    // Start a new combo attack
    void StartCombo()
    {
        mainAtkComboStream = 0;
        mainAtkComboStream++;
        lastClickTime = 0;

        // Perform the appropriate attack based on the current combo stream
        if (inputHandler.swordATKTriggered)
            PerformSwordAttack();
        else if (inputHandler.powerATKTriggered)
            PerformPowerAttack();
    }

    // Continue the current combo attack
    void ContinueCombo()
    {
        lastClickTime = 0;
        mainAtkComboStream++;

        if (inputHandler.swordATKTriggered)
            PerformSwordAttack();
        else if (inputHandler.powerATKTriggered)
            PerformPowerAttack();
    }

    // Reset the combo attack
    void ResetCombo()
    {
        Debug.Log("Combo reset");
        mainAtkComboStream = 0;
    }

    // Perform a sword attack
    void PerformSwordAttack()
    {
        if (swordAttacks.Count == 0)
        {
            Debug.Log("Empty");
        }
        // Play the corresponding animation for the sword attack
        playerAnimationHandler.SwordAttack(swordAttacks[mainAtkComboStream - 1]);
    }

    // Perform a power attack
    void PerformPowerAttack()
    {
        if (energyAttacks.Count == 0)
        {
            Debug.Log("Empty");
        }
        // Play the corresponding animation for the power attack
        playerAnimationHandler.PowerAttack(energyAttacks[mainAtkComboStream - 1]);
    }


    public AttackSO SwapSwordAttack(AttackSO newAttack)
    {
        int attackIndex = mainAtkComboStream;

        if (attackIndex < 0 || attackIndex >= swordAttacks.Count)
        {
            Debug.Log("invalid Attack index.");
            return null;
        }
        AttackSO oldAttack = swordAttacks[attackIndex];
        if (newAttack != null)
        {
            swordAttacks[mainAtkComboStream] = newAttack;
            Debug.Log($"Swapped attack at index {attackIndex}. Old Attack: {oldAttack.name}, New Attack: {newAttack.name}");
        }
        else
        {
            Debug.Log($"No new attack provided. Keeping the existing attack: {oldAttack.name}");
        }
        return oldAttack;
    }

    public EnergySO SwapEnergyAttack(EnergySO newAttack)
    {
        int attackIndex = mainAtkComboStream;

        if (attackIndex < 0 || attackIndex >= energyAttacks.Count)
        {
            Debug.Log("invalid Energy index.");
            return null;
        }
        EnergySO oldAttack = energyAttacks[attackIndex];
        if (newAttack != null)
        {
            energyAttacks[mainAtkComboStream] = newAttack;
            Debug.Log($"Swapped attack at index {attackIndex}. Old Attack: {oldAttack.name}, New Attack: {newAttack.name}");
        }
        else
        {
            Debug.Log($"No new attack provided. Keeping the existing attack: {oldAttack.name}");
        }
        return oldAttack;
    }
}
