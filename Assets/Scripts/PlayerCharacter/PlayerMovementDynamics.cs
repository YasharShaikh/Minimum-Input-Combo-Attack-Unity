using System.Collections;
using UnityEngine;

namespace player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementDynamics : MonoBehaviour
    {

        public static PlayerMovementDynamics instance;

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
        [Header("Blocking")]
        [SerializeField] LayerMask AvoidEnemyLayer;
        [SerializeField] float blockTime;
        [SerializeField] float sideStepDistance;
        [SerializeField] float dodgeDuration = 0.5f;
        [Space]
        float lastInput;
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
            StepDodge();
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

        private void StepDodge()
        {
            if (Mathf.Abs(playerInputHandler.moveInput.magnitude) > 0 && playerInputHandler.jumpTriggered)
            {
                // Determine the dodge direction and start the dodge coroutine
                Vector3 sideStepDirection = transform.forward * sideStepDistance;
                StartCoroutine(SmoothDodge(sideStepDirection));
            }
        }


        private IEnumerator SmoothDodge(Vector3 targetOffset)
        {
            // Time it takes to complete the dodge
            float elapsedTime = 0f;

            Vector3 startPosition = characterController.transform.localPosition;
            Vector3 targetPosition = startPosition + targetOffset;

            while (elapsedTime < dodgeDuration)
            {
                // Lerp the position smoothly over time
                characterController.Move(Vector3.Lerp(startPosition, targetPosition, elapsedTime / dodgeDuration) - characterController.transform.position);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait until the next frame
            }

            // Ensure final position after the dodge
            characterController.Move(targetPosition - characterController.transform.position);
        }
        private void ApplyGravity()
        {
            if (!characterController.isGrounded)
                characterController.Move(gravity * Time.deltaTime);
        }
    }
}

