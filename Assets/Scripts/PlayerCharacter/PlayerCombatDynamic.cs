using player;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatDynamic : MonoBehaviour
{
    public static PlayerCombatDynamic instance;

    [SerializeField] float timeBetweenCombo;
    int mainAtkComboStream;
    [SerializeField] int maxCombo = 3;
    float lastClickTime;

    [Header("Default Attacks")]
    [SerializeField] List<AttackSO> swordAttacks = new List<AttackSO>();
    [SerializeField] List<EnergySO> energyAttacks = new List<EnergySO>();



    [HideInInspector] public float swordAttackForwardStep = 0.0f;
    [HideInInspector] public float energyAttackForwardStep = 0.0f;


    bool isPerformingCombo;

    PlayerAnimationHandler playerAnimationHandler;
    PlayerInputHandler inputHandler;
    SoundManager soundManager;


    private void Awake()
    {
        instance = this;

        inputHandler = GetComponent<PlayerInputHandler>();
        playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
        soundManager = GetComponentInChildren<SoundManager>();
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
        if (CheckAttackPerform())
        {
            isPerformingCombo = true;
            // If the current main attack combo stream is zero, start a new combo
            if (mainAtkComboStream == 0 || mainAtkComboStream == maxCombo)
                StartCombo();
            else if (mainAtkComboStream > 0 && mainAtkComboStream < maxCombo)
            {
                // Check if the time between combo attacks has elapsed
                if (lastClickTime < timeBetweenCombo)
                    //Continue the combo attack
                    ContinueCombo();
                else
                    // Start a new combo attack
                    StartCombo();
            }
        }
    }

    // Start a new combo attack
    void StartCombo()
    {
        mainAtkComboStream = 0;
        mainAtkComboStream++;
        lastClickTime = 0;

        PerformAttack();
    }

    // Continue the current combo attack
    void ContinueCombo()
    {
        lastClickTime = 0;
        mainAtkComboStream++;

        PerformAttack();
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
            swordAttackForwardStep = 0.0f;
            return;
        }

        // Play the corresponding animation for the sword attack
        playerAnimationHandler.SwordAttack(swordAttacks[mainAtkComboStream - 1]);
        swordAttackForwardStep = swordAttacks[mainAtkComboStream - 1].forwardStep;
        soundManager.PlaySwordClip();
    }

    // Perform a power attack
    void PerformPowerAttack()
    {
        if (energyAttacks.Count == 0)
            return;
        EnergySO selectedAttack = energyAttacks[mainAtkComboStream - 1];
        EnergyHandler energyHandler = GetHandlerForType(selectedAttack.energyType);

        if (energyHandler != null)
        {
            energyHandler.Execute(selectedAttack, transform, GetTarget());
        }
        // Play the corresponding animation for the power attack
        playerAnimationHandler.PowerAttack(energyAttacks[mainAtkComboStream - 1]);
        energyAttackForwardStep = energyAttacks[mainAtkComboStream - 1].forwardStep;
    }
    private EnergyHandler GetHandlerForType(EnergyType type)
    {
        switch (type)
        {
            case EnergyType.Projectile:
                return gameObject.AddComponent<ProjectileMagic>();
            case EnergyType.Stun:
                return gameObject.AddComponent<StunMagic>();

            //case EnergyType.Tornado:
            //    //return gameObject.AddComponent<TornadoMagic>();
            default:
                Debug.LogError("Unsupported energy type!");
                return null;
        }
    }
    private Transform GetTarget()
    {
        // Implement logic to find or aim at a target if required
        return null;
    }
    public AttackSO SwapSwordAttack(AttackSO newAttack)
    {
        return swapAttack(swordAttacks, newAttack);
    }
    public EnergySO SwapEnergyAttack(EnergySO newAttack)
    {
        return swapAttack(energyAttacks, newAttack);
    }


    #region Class Helpers

    private bool CheckAttackPerform()
    {
        return (inputHandler.swordATKTriggered || inputHandler.powerATKTriggered) && !playerAnimationHandler.isPerformingSwordAttack && !playerAnimationHandler.isPerformingEnergyAttack;
    }
    private void PerformAttack()
    {
        if (inputHandler.swordATKTriggered)
            PerformSwordAttack();
        else if (inputHandler.powerATKTriggered)
            PerformPowerAttack();
    }

    public T swapAttack<T>(List<T> attackList, T newAttack) where T : ScriptableObject
    {
        int attackIndex = mainAtkComboStream;
        if (attackIndex < 0 || attackIndex >= attackList.Count)
        {
            Debug.Log("invalid Attack index.");
            return null;
        }
        T oldAttack = attackList[attackIndex];
        if (newAttack != null)
        {
            attackList[mainAtkComboStream] = newAttack;
            Debug.Log($"Swapped attack at index {attackIndex}. Old Attack: {oldAttack.name}, New Attack: {newAttack.name}");
        }
        else
        {
            Debug.Log($"No new attack provided. Keeping the existing attack: {oldAttack.name}");
        }
        return oldAttack;
    }

    #endregion
}
