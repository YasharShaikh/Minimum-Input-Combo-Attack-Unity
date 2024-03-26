using player;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{

    [SerializeField] float timeBetweenCombo;
    [SerializeField] int mainAtkComboStream; // will track the current combo count
    [SerializeField] string[] swdATKTracker = { "sword attack 1", "sword attack 2", "sword attack 3" }; // will track which swd atk animation is to be played
    [SerializeField] string[] pwrATKTracker = { "power attack 1", "power attack 2", "power attack 3" }; // will track which pwr atk animation is to be played
    [SerializeField] string[] performedComboTracker;
    int maxCombo = 3;
    float lastClickTime;
    bool isPerformingCombo;
    bool isPerformingATK;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //check if button pressed swd or pwr
        //check if this is a fresh combo attack then set isPerformingCombo=true
        //if current mainATKcombostream is 1 and button pressed is swd then perform 1st swd || current mainATKcombostream is 1 and button pressed is pwr then perform 1st pwr
        //similarly for mainATKcombostream 2 and mainATKcombostream 3 


        if (InputHandler.instance.swordATKTriggered || InputHandler.instance.powerATKTriggered)
        {
            if(mainAtkComboStream == 0)
            {

            }
            mainAtkComboStream++;
            Debug.Log("Current combo count= " + mainAtkComboStream);
        }

    }


}
