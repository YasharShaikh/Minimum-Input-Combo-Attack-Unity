using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public partial class PlayerStateManager : MonoBehaviour
{
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerCamera = Camera.main;


        playerAnimator = GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Move();
        ApplyGravity();
    }

    private void FixedUpdate()
    {
    }

    #region movement

    private void ApplyGravity()
    {
        characterController.Move(gravity * Time.deltaTime);
    }
    private void Move()
    {
        float horizontalInput = playerInput.actions["Movement"].ReadValue<Vector2>().x;
        float verticalInput = playerInput.actions["Movement"].ReadValue<Vector2>().y;
        
        Vector3 moveDirection = (playerCamera.transform.forward * verticalInput) +
                                (playerCamera.transform.right * horizontalInput);

        float magnitude = Mathf.Clamp01(moveDirection.magnitude)*moveSpeed;

        playerAnimator.SetFloat("velocity", magnitude);
        moveDirection.y = 0f;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    #endregion
}
