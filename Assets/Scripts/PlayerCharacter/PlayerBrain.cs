using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
namespace player
{
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerBrain : MonoBehaviour
    {

        public static PlayerBrain instance;

        [Header("Player Movement")]
        [SerializeField] Vector3 gravity;
        [SerializeField] float moveSpeed;
        [SerializeField] float rotationSpeed;





        [Space]
        [HideInInspector] public float magnitude;
        [SerializeField] float moveVertical;
        [SerializeField] float moveHorizontal;
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
            Move();
            ApplyGravity();
        }



        private void Move()
        {
            moveVertical = InputHandler.instance.moveInput.x;
            moveHorizontal = InputHandler.instance.moveInput.y;


            Vector3 moveDirection = (playerCamera.transform.right * moveVertical) + (playerCamera.transform.forward * moveHorizontal);


            magnitude =  Mathf.Clamp01(moveDirection.magnitude) * moveSpeed;

            moveDirection.y = 0f;
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        private void ApplyGravity()
        {
            // Apply gravity to the character controller
            characterController.Move(gravity * Time.deltaTime);
        }
    }
}

