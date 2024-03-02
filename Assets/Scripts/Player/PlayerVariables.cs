using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public partial class PlayerStateManager
{

    [Header("Movement Data")]
    [HideInInspector] Vector3 moveVector;
    [HideInInspector] Vector2 inputVector;



    [Header("Character Movement")]
    [SerializeField] Transform followTarget;
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float playerRotation;
    [Space]
    [SerializeField] Vector3 gravity;
    [Header("Player animations")]
    [SerializeField] int VelocityHash;


    [Header("Camera Controller")]
    [SerializeField] float rotationSpeed;
    [HideInInspector] Camera playerCamera;


    [Header("References")]
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] Animator playerAnimator;


}
