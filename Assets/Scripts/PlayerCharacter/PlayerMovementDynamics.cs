using UnityEngine;

namespace player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementDynamics : MonoBehaviour
    {

        public static PlayerMovementDynamics instance;
        [SerializeField] float lastInput;

        [Header("Information collection")]
        [SerializeField] Transform playerHead;
        [SerializeField] float RayDistance;


        [Header("Movement Stats")]
        [SerializeField] Vector3 gravity;
        [SerializeField] Vector3 moveDirection;
        [SerializeField] float moveSpeed;
        [SerializeField] float moveAttackSpeed;
        [SerializeField] float rollSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] float sideStepDistance;


        [Space]
        float magnitude;
        CharacterController characterController;
        Camera playerCamera;
        PlayerAnimationHandler playerAnimationHandler;
        PlayerInputHandler playerInputHandler;
        PlayerCombatDynamic playerCombatDynamic;

        private void Awake()
        {
            instance = this;
            characterController = GetComponent<CharacterController>();
            playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
            playerInputHandler = GetComponent<PlayerInputHandler>();
            playerCombatDynamic = GetComponent<PlayerCombatDynamic>();
            playerCamera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            ApplyGravity();
        }

        private void Move()
        {
            Vector3 inputDirection = new Vector3(playerInputHandler.moveInput.x, 0, playerInputHandler.moveInput.y);
            Vector3 cameraDirection = playerCamera.transform.TransformDirection(inputDirection);
            cameraDirection.y = 0;

            moveDirection = cameraDirection.normalized * moveSpeed;
            magnitude = moveDirection.magnitude;

            PlayerAnimationHandler.instance.LocomotionAnimaton(magnitude);

            if (magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            if (playerAnimationHandler.isPerformingEnergyAttack || playerAnimationHandler.isPerformingSwordAttack)
            {
                Vector3 forwardStepDirection = transform.forward * playerCombatDynamic.swordAttackForwardStep;
                characterController.Move(forwardStepDirection * Time.deltaTime);
            }
            else
            {
                characterController.Move(moveDirection * Time.deltaTime);
            }
        }


        private void ApplyGravity()
        {
            if (!characterController.isGrounded)
                characterController.Move(gravity * Time.deltaTime);
        }
    }
}

