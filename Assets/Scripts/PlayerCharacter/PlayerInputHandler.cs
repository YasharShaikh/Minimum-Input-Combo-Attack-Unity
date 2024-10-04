using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
    public class PlayerInputHandler : MonoBehaviour
    {

        public static PlayerInputHandler instance;
        [Header("InputAction Asset")]
        [SerializeField] InputActionAsset playerControls;

        [Header("ActionMap Name")]
        [SerializeField] string actionMapName;


        #region[New Input strings]
        [Header("Movement")]
        [SerializeField] string move;

        [Header("Movement Action")]
        [SerializeField] string roll;
        [SerializeField] string jump;

        [Header("Combat Action")]
        [SerializeField] string swordAttack;
        [SerializeField] string powerAttack;
        [SerializeField] string lockON;
        #endregion



        InputAction moveAction;

        InputAction rollAction;
        InputAction jumpAction;
        
        InputAction swordAction;
        InputAction powerAction;
        InputAction lockONAction;

        public Vector2 moveInput { get; private set; }
        public bool swordATKTriggered { get; private set; }
        public bool powerATKTriggered { get; private set; }
        public bool rollTriggered { get; private set; }
        public bool jumpTriggered { get; private set; }
        public bool lockONTriggered { get; private set; }

        private void Awake()
        {
            instance = this;

            moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
            rollAction = playerControls.FindActionMap(actionMapName).FindAction(roll);
            jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
            

            swordAction = playerControls.FindActionMap(actionMapName).FindAction(swordAttack);
            powerAction = playerControls.FindActionMap(actionMapName).FindAction(powerAttack);

            lockONAction = playerControls.FindActionMap(actionMapName).FindAction(lockON);

            RegisterInputActions();
        }

        private void Update()
        {
            moveInput = moveAction.ReadValue<Vector2>();

            rollTriggered = rollAction.triggered;
            jumpTriggered = jumpAction.triggered;   
            
            swordATKTriggered = swordAction.triggered;
            powerATKTriggered = powerAction.triggered;

            if (lockONAction.triggered)
                lockONTriggered = !lockONTriggered;
        }
        private void RegisterInputActions()
        {
            moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
            moveAction.canceled += context => moveInput = Vector2.zero;
        }

        private void OnEnable()
        {
            moveAction.Enable();

            rollAction.Enable();
            jumpAction.Enable();

            swordAction.Enable();
            powerAction.Enable();

            lockONAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();

            rollAction.Disable();
            jumpAction.Disable();

            swordAction.Disable();
            powerAction.Disable();

            lockONAction.Disable();
        }
    }
}

