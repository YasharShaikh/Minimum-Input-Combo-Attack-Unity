using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace player
{
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerBrain : MonoBehaviour
    {

        public static PlayerBrain instance;

        [Header("Information collection")]
        [SerializeField] Transform playerHead;
        [SerializeField] float RayDistance;


        [Header("Player Movement")]
        [SerializeField] Vector3 gravity;
        [SerializeField] Vector3 moveDirection;
        [SerializeField] float moveSpeed;
        [SerializeField] float rotationSpeed;
        [HideInInspector] public bool isPerformingROLLAnimation;

        [Header("Player Stats")]
        [SerializeField] float maxStamina;
        [SerializeField] float currentStamina;
        [SerializeField] float regenerationRate;
        [SerializeField] float deductionRateROLL;
            
        [Space]
        [HideInInspector] public float magnitude;
        CharacterController characterController;
        Camera playerCamera;


        private void Awake()
        {
            instance = this;
            characterController = GetComponent<CharacterController>();
            playerCamera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            PlayerVisionInfo();
            Move();
            ApplyGravity();
            PerformRoll();
        }


        private void PlayerVisionInfo()
        {
            int layerMask = ~LayerMask.GetMask("Player");
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,RayDistance,layerMask))
            {
                Debug.DrawRay(ray.origin, playerCamera.transform.forward, Color.red);
                Debug.Log("Hit: " + hit.transform.name);
            }
        }

        private void Move()
        {
            float moveVertical = InputHandler.instance.moveInput.x;
            float moveHorizontal = InputHandler.instance.moveInput.y;

            PlayerAnimationHandler.instance.LocomotionAnimaton(magnitude);
            moveDirection = (playerCamera.transform.right * moveVertical) + (playerCamera.transform.forward * moveHorizontal);
            magnitude = Mathf.Clamp01(moveDirection.magnitude) * moveSpeed;

            moveDirection.y = 0f;
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        private void PerformRoll()
        {
            if (isPerformingROLLAnimation)
                return;
            if(magnitude > 0 && InputHandler.instance.rollTriggered)
            {
                isPerformingROLLAnimation = true;
                moveDirection.y = 0;
                moveDirection.Normalize();

                Quaternion lookDirection = Quaternion.LookRotation(moveDirection);

                Debug.Log("performing roll");
                PlayerAnimationHandler.instance.rollPerform();
            }
           


            //full logic for perform roll

        }
        private void ApplyGravity()
        {
            // Apply gravity to the character controller
            characterController.Move(gravity * Time.deltaTime);
        }
    }
}

