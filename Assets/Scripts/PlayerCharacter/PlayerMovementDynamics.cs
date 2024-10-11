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
        [HideInInspector] public bool isPerformingROLLAnimation;

        [Header("Jump Stats")]
        [SerializeField] float jumpForce;
        [SerializeField] float jumpGravity;
        [HideInInspector] public bool isPerformingJumpAnimation;
        [Space]
        [SerializeField] float initialJumpVelocity;
        [SerializeField] float maxJumpHeight;
        [SerializeField] float maxJumpTime;

        [Header("Lock ON ")]
        [SerializeField] float lockONRadius;
        [SerializeField] float minLockONAngle;
        [SerializeField] float maxLockONAngle;
        [SerializeField] float targetDistanceLockON;
        [SerializeField] GameObject lockedONEnemy;

        [Space]
        [HideInInspector] public float magnitude;
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
            SetupJumpVar();
        }

        // Update is called once per frame
        void Update()
        {

            PerformJump();
            Move();
            ApplyGravity();
            LockONHandler();
        }


        private void PlayerVisionInfo()
        {
            int layerMask = ~LayerMask.GetMask("Player");
            //Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, RayDistance, enemyLayerMask))
            //{
            //    Debug.DrawRay(ray.origin, playerCamera.transform.forward, Color.red);
            //    //LockONHandler(hit);
            //}
        }


        private void LockONHandler()
        {


            int enemyLayerMask = LayerMask.GetMask("enemy");
            Collider[] enemyColliders = Physics.OverlapSphere(transform.position, lockONRadius, enemyLayerMask);
            foreach (Collider enemyCollider in enemyColliders)
            {

                EnemyBrain enemy = enemyCollider.GetComponentInParent<EnemyBrain>();
                Vector3 enemyDirection = enemyCollider.transform.position - transform.position;
                float playerDistanceFromEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);
                float viewableAngle = Vector3.Angle(enemyDirection, playerCamera.transform.forward);


                //Debug.Log("Eneny name = " +enemy.name + "angle = " + viewableAngle);
                Debug.DrawRay(transform.position, enemyDirection, Color.red);

                //if (enemy == null)
                //    Debug.Log("bRAIN Found = "+ playerDistanceFromEnemy);
                //if (!enemy.isDead)
                //    Debug.Log("enemy=" + enemy.gameObject.name);

                //continue;
                //if (playerDistanceFromEnemy > targetDistanceLockON)
                //    continue;

                if (viewableAngle > minLockONAngle && viewableAngle < maxLockONAngle)
                {
                    Debug.DrawRay(transform.position, enemyDirection, Color.red);
                    enemy.isDead = true;
                }




            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lockONRadius);
        }
        //private static void LockONHandler(RaycastHit hit)
        //{
        //    if (hit.transform.tag == "enemy")
        //    {
        //        if (InputHandler.instance.lockONTriggered)
        //        {
        //            Debug.Log("Enemy locked on ");
        //        }
        //        else if (!InputHandler.instance.lockONTriggered)
        //        {
        //            Debug.Log("Enemy lock on removed");
        //        }
        //    }
        //}

        private void Move()
        {
            float moveVertical = PlayerInputHandler.instance.moveInput.x;
            float moveHorizontal = PlayerInputHandler.instance.moveInput.y;

            PlayerAnimationHandler.instance.LocomotionAnimaton(magnitude);

            moveDirection = (playerCamera.transform.right * moveVertical) + (playerCamera.transform.forward * moveHorizontal);

            magnitude = Mathf.Clamp01(moveDirection.magnitude) * moveSpeed;

            moveDirection.y = 0f;

            if (playerAnimationHandler.isPerformingEnergyAttack || playerAnimationHandler.isPerformingSwordAttack)
            {
                Vector3 forwardStepDirection = transform.forward * playerCombatDynamic.swordAttackForwardStep;

                characterController.Move(forwardStepDirection * Time.deltaTime);
            }
            else
            {
                if (moveDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }

                characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            }
        }



        private void SetupJumpVar()
        {
            float timeToApex = maxJumpTime / 2;
            initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        }

        private void PerformJump()
        {
            if (isPerformingJumpAnimation || !characterController.isGrounded)
                return;

            if (characterController.isGrounded && PlayerInputHandler.instance.jumpTriggered)
            {
                Debug.Log("Performing Jump");
                playerAnimationHandler.PerformJumpStart();

                Vector3 _jumpVelocity = new Vector3(0, initialJumpVelocity, 0);
                characterController.Move(_jumpVelocity * Time.deltaTime);
            }


        }
        private void PerformRoll()
        {
            if (isPerformingROLLAnimation)
                return;
            if (magnitude > 0 && PlayerInputHandler.instance.rollTriggered)
            {
                isPerformingROLLAnimation = true;
                moveDirection.y = 0;
                moveDirection.Normalize();

                Quaternion lookDirection = Quaternion.LookRotation(moveDirection);
                //characterController.Move(moveDirection.normalized * rollSpeed * Time.deltaTime);


                playerAnimationHandler.rollPerform();
            }



            //full logic for perform roll

        }
        private void ApplyGravity()
        {
            if (!characterController.isGrounded)
                characterController.Move(gravity * Time.deltaTime);
        }
    }
}

