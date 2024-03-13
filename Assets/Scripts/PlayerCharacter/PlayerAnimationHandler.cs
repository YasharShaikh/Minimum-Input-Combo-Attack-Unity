using player;
using UnityEngine;



public class PlayerAnimationHandler : MonoBehaviour
{
    public static PlayerAnimationHandler instance;
    Animator playerAnimator;
    [Header("Animation State")]
    [SerializeField] int BasicMovementLayer;
    [SerializeField] int AttackLayer;




    private void Awake()
    {
        instance = this;
        playerAnimator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        swordAttack();
    }

   

    #region combat related



    private void swordAttack()
    {
        bool swordAttack = InputHandler.instance.swordATKTriggered;
        if (swordAttack)
        {
            playerAnimator.SetTrigger("swdATK");
        }
    }
    private void powerAttack()
    {
        bool powerAttack = InputHandler.instance.powerATKTriggered;
        if (powerAttack)
        {
            playerAnimator.SetTrigger("engyATK");
        }
    }
    #endregion

    #region movement related
    public void LocomotionAnimaton(float magnitude)
    {
        playerAnimator.SetFloat("velocity", magnitude);
    }

    #endregion

    #region action related
    public void rollPerform()
    {
        playerAnimator.SetTrigger("roll");

    }
    #endregion

}
