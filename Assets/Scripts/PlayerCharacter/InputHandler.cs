using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
    public class InputHandler : MonoBehaviour
    {

        public static InputHandler instance;
        [Header("Inpu tAction Asset")]
        [SerializeField] InputActionAsset playerControls;

        [Header("Action Map Name")]
        [SerializeField] string actionMapName;

        [Header("Action Name ref")]
        [SerializeField] string move;
        [SerializeField] string roll;
        [SerializeField] string swordAttack;
        [SerializeField] string powerAttack;

        InputAction moveAction;
        InputAction rollAction;
        InputAction swordAction;
        InputAction powerAction;



        public Vector2 moveInput { get; private set; }
        public bool swordATKTriggered { get; private set; }
        public bool powerATKTriggered { get; private set; }
        public bool rollTriggered { get; private set; }

        private void Awake()
        {
            instance = this;

            moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
            rollAction = playerControls.FindActionMap(actionMapName).FindAction(roll);

            swordAction = playerControls.FindActionMap(actionMapName).FindAction(swordAttack);
            powerAction = playerControls.FindActionMap(actionMapName).FindAction(powerAttack);


            RegisterInputActions();
        }

        private void Update()
        {
            moveInput = moveAction.ReadValue<Vector2>();
            swordATKTriggered = swordAction.triggered;
            powerATKTriggered = powerAction.triggered;
            rollTriggered = rollAction.triggered;   

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
            swordAction.Enable();
            powerAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            rollAction.Disable();
            swordAction.Disable();
            powerAction.Disable();
        }
    }
}

