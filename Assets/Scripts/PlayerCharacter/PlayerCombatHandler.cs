using player;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    [SerializeField] float timeBetweenCombo;
    [SerializeField] int mainAtkComboStream; // will track the current combo count
    [SerializeField] List< string> swdATKTrackerL = new List<string>() ; // will track which swd atk animation is to be played
    [SerializeField] List<string> pwrATKTrackerL  = new List<string>(); // will track which pwr atk animation is to be played
    [SerializeField] string[] performedComboTracker;
    int maxCombo = 3;
    [SerializeField] float lastClickTime;
    bool isPerformingCombo;
    bool isPerformingATK;

    // Start is called before the first frame update
    void Start()
    {
        mainAtkComboStream = 0;
        lastClickTime = 0f;
        isPerformingCombo = false;
        isPerformingATK = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPerformingCombo )
        {
            lastClickTime = 0;
        }
        else
        {
            lastClickTime += Time.deltaTime;
        }

        // Check if the player has triggered a sword or power attack
        if (InputHandler.instance.swordATKTriggered || InputHandler.instance.powerATKTriggered)
        {
            isPerformingCombo = true;
            // If the current main attack combo stream is zero, start a new combo
            if (mainAtkComboStream == 0)
            {
                StartCombo();
            }
            else if (mainAtkComboStream < maxCombo)
            {
                // Check if the time between combo attacks has elapsed
                if ( lastClickTime < timeBetweenCombo)
                {
                    // Continue the combo attack
                    ContinueCombo();
                }
                else
                {
                    Debug.Log("reset combo due to timebetweencombo");
                    // Start a new combo attack
                    StartCombo();
                }
            }
            else
            {
                // Maximum combo reached, reset combo stream
                ResetCombo();
            }

            // Update the last click time
        }
    }

    // Start a new combo attack
    void StartCombo()
    {
        mainAtkComboStream = 0;
        mainAtkComboStream++;
        Debug.Log("Starting combo " + mainAtkComboStream);

        // Perform the appropriate attack based on the current combo stream
        if (InputHandler.instance.swordATKTriggered)
        {
            PerformSwordAttack();
        }
        else if (InputHandler.instance.powerATKTriggered)
        {
            PerformPowerAttack();
        }
    }

    // Continue the current combo attack
    void ContinueCombo()
    {
        Debug.Log("Continuing combo " + mainAtkComboStream);

        // Perform the appropriate attack based on the current combo stream
        if (InputHandler.instance.swordATKTriggered)
        {
            PerformSwordAttack();
        }
        else if (InputHandler.instance.powerATKTriggered)
        {
            PerformPowerAttack();
        }
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
        if(swdATKTrackerL.Count == 0)
        {
            Debug.Log("Empty");
        }
        Debug.Log("Performing sword attack from list=" + swdATKTrackerL[mainAtkComboStream-1]);
        // Play the corresponding animation for the sword attack
    }

    // Perform a power attack
    void PerformPowerAttack()
    {
        if (pwrATKTrackerL.Count == 0)
        {
            Debug.Log("Empty");
        }
        Debug.Log("Performing power attack: " + pwrATKTrackerL[mainAtkComboStream - 1]);
        // Play the corresponding animation for the power attack
    }
}
