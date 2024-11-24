using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public static PlayerInputHandler instance;

        [Header("InputAction Asset")]
        [SerializeField] private InputActionAsset playerControls;

        [Header("ActionMap Name")]
        [SerializeField] private string actionMapName;

        #region Input Action Strings
        [Header("Movement")]
        [SerializeField] private string move;

        [Header("Movement Actions")]
        [SerializeField] private string roll;
        [SerializeField] private string jump;

        [Header("Combat Actions")]
        [SerializeField] private string swordAttack;
        [SerializeField] private string powerAttack;
        [SerializeField] private string lockON;
        #endregion

        // Input actions
        private InputAction moveAction;
        private InputAction rollAction;
        private InputAction jumpAction;
        private InputAction swordAction;
        private InputAction powerAction;
        private InputAction lockONAction;

        // Public properties for input states
        public Vector2 moveInput { get; private set; }
        public bool swordATKTriggered { get; private set; }
        public bool powerATKTriggered { get; private set; }
        public bool rollTriggered { get; private set; }
        public bool jumpTriggered { get; private set; }
        public bool lockONTriggered { get; private set; }

        private void Awake()
        {
            instance = this;

            // Initialize input actions
            var actionMap = playerControls.FindActionMap(actionMapName);
            moveAction = actionMap.FindAction(move);
            rollAction = actionMap.FindAction(roll);
            jumpAction = actionMap.FindAction(jump);
            swordAction = actionMap.FindAction(swordAttack);
            powerAction = actionMap.FindAction(powerAttack);
            lockONAction = actionMap.FindAction(lockON);

            RegisterInputActions();
        }

        private void Update()
        {
            // Update movement and triggered states
            moveInput = moveAction.ReadValue<Vector2>();

            rollTriggered = rollAction.triggered;
            jumpTriggered = jumpAction.triggered;
            swordATKTriggered = swordAction.triggered;
            powerATKTriggered = powerAction.triggered;

            // Toggle Lock-On
            if (lockONAction.triggered)
            {
                lockONTriggered = !lockONTriggered;
            }
        }

        private void RegisterInputActions()
        {
            // Handle movement inputs
            moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
            moveAction.canceled += context => moveInput = Vector2.zero;
        }

        private void OnEnable()
        {
            // Enable all input actions
            moveAction.Enable();
            rollAction.Enable();
            jumpAction.Enable();
            swordAction.Enable();
            powerAction.Enable();
            lockONAction.Enable();
        }

        private void OnDisable()
        {
            // Disable all input actions
            moveAction.Disable();
            rollAction.Disable();
            jumpAction.Disable();
            swordAction.Disable();
            powerAction.Disable();
            lockONAction.Disable();
        }
    }
}
