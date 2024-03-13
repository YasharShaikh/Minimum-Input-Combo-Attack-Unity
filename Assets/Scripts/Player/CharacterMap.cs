using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager : MonoBehaviour
{
    [Header("Player movement Inputs")]
    [SerializeField] Vector2 moveInput;
    public float verticalInput;
    public float horizontalInput;

    [Header("Player Action Inputs")]
    [SerializeField] bool rollInput = false;
    [SerializeField] bool swdATK = false;
    [SerializeField] bool pwrATK = false;



    private void OnMovement(InputValue input)
    {
        // Read movement input values
        moveInput = input.Get<Vector2>();
        verticalInput = moveInput.x;
        horizontalInput = moveInput.y;

    }

    private void OnSwordAttack(InputAction.CallbackContext context)
    {
        // Set sword attack flag to true when the action is performed
        swdATK = context.performed;
    }

    private void OnPowerAttack(InputAction.CallbackContext context)
    {
        // Set power attack flag to true when the action is performed
        pwrATK = context.performed;
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rollInput = true;
        }
        else if (context.canceled)
        {
            rollInput = false;
        }
    }


    private void HandleMovement()
    {
        // Handle player movement based on input values
        Vector3 moveDirection = (playerCamera.transform.forward * verticalInput) +
                                (playerCamera.transform.right * horizontalInput);

        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * moveSpeed;
        playerAnimator.SetFloat("velocity", magnitude);

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
