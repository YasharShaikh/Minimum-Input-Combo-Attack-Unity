using System.Collections;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

namespace player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementDynamics : MonoBehaviour
    {
        public static PlayerMovementDynamics instance;

        [Header("Information Collection")]
        [SerializeField] private Transform playerHead;
        [SerializeField] private float rayDistance;

        [Header("Movement Stats")]
        [SerializeField] private Vector3 gravity;
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float moveAttackSpeed;
        [SerializeField] private float rollSpeed;
        [SerializeField] private float rotationSpeed;

        [Header("Blocking")]
        [SerializeField] private LayerMask avoidEnemyLayer;
        [SerializeField] private float blockTime;
        [SerializeField] private float sideStepDistance;
        [SerializeField] private float dodgeDuration = 0.5f;

        [Header("Lock On")]
        [SerializeField] private bool enemyLocked = false;
        [SerializeField] private GameObject POV;
        [SerializeField] private float lockOnRadius;

        [Header("Camera Manager")]
        [SerializeField] private CinemachineCamera freelookCamera;
        [SerializeField] private CinemachineCamera lockonCamera;
        [SerializeField] private Collider closestEnemy = null;
        [SerializeField] private float closestDistance;
        private float lastInput;
        private float magnitude;

        private CharacterController characterController;
        private Camera playerCamera;
        private PlayerAnimationHandler playerAnimationHandler;
        private PlayerInputHandler playerInputHandler;
        private PlayerCombatDynamic playerCombatDynamic;

        private void Awake()
        {
            instance = this;
            characterController = GetComponent<CharacterController>();
            playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
            playerInputHandler = GetComponent<PlayerInputHandler>();
            playerCombatDynamic = GetComponent<PlayerCombatDynamic>();
            playerCamera = Camera.main;
            closestDistance = float.MaxValue;
        }

        private void Update()
        {
            ProcessMovement();
            HandleDodge();
            ApplyGravity();
            CheckEnemiesInFOV();
        }

        private void ProcessMovement()
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

        private void HandleDodge()
        {
            if (Mathf.Abs(playerInputHandler.moveInput.magnitude) > 0 && playerInputHandler.jumpTriggered)
            {
                Vector3 sideStepDirection = transform.forward * sideStepDistance;
                StartCoroutine(SmoothDodge(sideStepDirection));
            }
        }

        private void CheckEnemiesInFOV()
        {
            if (playerInputHandler.lockONTriggered)
            {
                if (!enemyLocked)
                {
                    // Reset closest enemy variables
                    closestEnemy = null;
                    closestDistance = Mathf.Infinity;

                    int enemyLayerMask = LayerMask.GetMask("Enemy");
                    Collider[] colliders = Physics.OverlapSphere(POV.transform.position, lockOnRadius, enemyLayerMask);

                    foreach (Collider collider in colliders)
                    {
                        Enemy enemyBrain = collider.GetComponent<Enemy>();
                        if (enemyBrain == null || !enemyBrain.isAlive) continue;

                        Vector3 toEnemy = (collider.transform.position - playerCamera.transform.position).normalized;
                        float dotProduct = Vector3.Dot(playerCamera.transform.forward, toEnemy);

                        // Check if the enemy is within the FOV
                        if (dotProduct > Mathf.Cos(Mathf.Deg2Rad * 90.0f))
                        {
                            if (Physics.Raycast(playerCamera.transform.position, toEnemy, out RaycastHit hit, lockOnRadius) && hit.collider == collider)
                            {
                                float distanceToEnemy = Vector3.Distance(playerCamera.transform.position, collider.transform.position);
                                if (distanceToEnemy < closestDistance)
                                {
                                    closestDistance = distanceToEnemy;
                                    closestEnemy = collider;
                                }
                            }
                        }
                    }

                    if (closestEnemy != null)
                    {
                        Debug.Log($"Locking on to {closestEnemy.transform.root.name}");
                        freelookCamera.gameObject.SetActive(false);
                        lockonCamera.gameObject.SetActive(true);
                        lockonCamera.LookAt = closestEnemy.transform;
                        enemyLocked = true;
                    }
                    else
                    {
                        Debug.Log("No valid enemies in range.");
                    }
                }
               
            }
            else if(!playerInputHandler.lockONTriggered)
            {
                ResetLockOn();
            }
        }

        private void ResetLockOn()
        {
            freelookCamera.gameObject.SetActive(true);
            lockonCamera.gameObject.SetActive(false);
            lockonCamera.LookAt = null;
            closestEnemy = null;
            closestDistance = Mathf.Infinity;
            enemyLocked = false;
        }


        private void ApplyGravity()
        {
            if (!characterController.isGrounded)
                characterController.Move(gravity * Time.deltaTime);
        }

        private IEnumerator SmoothDodge(Vector3 targetOffset)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = characterController.transform.localPosition;
            Vector3 targetPosition = startPosition + targetOffset;

            while (elapsedTime < dodgeDuration)
            {
                characterController.Move(Vector3.Lerp(startPosition, targetPosition, elapsedTime / dodgeDuration) - characterController.transform.position);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            characterController.Move(targetPosition - characterController.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.yellow;
            Handles.DrawWireArc(POV.transform.position, Vector3.up, Vector3.left, 180, lockOnRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(POV.transform.position, lockOnRadius);
        }
    }
}
