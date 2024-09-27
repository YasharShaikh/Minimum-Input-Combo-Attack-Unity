using player;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    [SerializeField] int maxCombo;
    [SerializeField] int currentCombo;

    [Header("Combo Time")]
    [SerializeField] float currentTime;
    [SerializeField] float maxTime;
    bool bisPerformingCombo;
    bool bIsPerformingENGAttack;

    PlayerInputHandler InputHandler;
    PlayerAnimationHandler PlayerAnimationHandler;
    public AnimationEvent swdAttack;


    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerAnimationHandler = GetComponent<PlayerAnimationHandler>();    
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PerformSWDAttack(InputHandler.swordATKTriggered);
    }


    void PerformSWDAttack(bool input)
    {
        if (input) //also check if the current attack is completed or not
        {
            if(bisPerformingCombo) 
            {
                currentCombo++;
                //restart timer for next attack
                //play next animation from the combo
            }
            else
            {
                currentCombo++;
            }
        }
    }

    void HandleComboTime(float currentTime)
    {

    }
}
