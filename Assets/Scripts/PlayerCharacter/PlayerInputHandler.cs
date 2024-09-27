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

        [Header("Action Name ref")]
        [SerializeField] string move;



        [Header("Movement Action")]
        [SerializeField] string roll;


        [Header("Combat Action")]
        [SerializeField] string swordAttack;
        [SerializeField] string powerAttack;
        [SerializeField] string lockON;


        InputAction moveAction;
        InputAction rollAction;
        InputAction swordAction;
        InputAction powerAction;
        InputAction lockONAction;

        public Vector2 moveInput { get; private set; }
        public bool swordATKTriggered { get; private set; }
        public bool powerATKTriggered { get; private set; }
        public bool rollTriggered { get; private set; }
        public bool lockONTriggered { get; private set; }

        private void Awake()
        {
            instance = this;

            moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
            rollAction = playerControls.FindActionMap(actionMapName).FindAction(roll);

            swordAction = playerControls.FindActionMap(actionMapName).FindAction(swordAttack);
            powerAction = playerControls.FindActionMap(actionMapName).FindAction(powerAttack);

            lockONAction = playerControls.FindActionMap(actionMapName).FindAction(lockON);

            RegisterInputActions();
        }

        private void Update()
        {
            moveInput = moveAction.ReadValue<Vector2>();
            swordATKTriggered = swordAction.triggered;
            powerATKTriggered = powerAction.triggered;
            rollTriggered = rollAction.triggered;
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

            lockONAction.Enable();
            rollAction.Enable();

            swordAction.Enable();
            powerAction.Enable();

        }

        private void OnDisable()
        {
            moveAction.Disable();

            lockONAction.Disable();
            rollAction.Disable();

            swordAction.Disable();
            powerAction.Disable();


        }
    }
}
