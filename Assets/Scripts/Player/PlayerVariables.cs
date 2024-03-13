using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputs))]

public partial class PlayerStateManager
{

    [Header("Movement Data")]



    [Header("Character Movement")]
    [SerializeField] Transform followTarget;
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float playerRotation;
    [Header("Character Action Input")]
    [SerializeField] Vector2 rollDirection;
    [Space]
    [SerializeField] Vector3 gravity;
    [Header("Player animations")]
    [Space]
    public bool SwdAttack;
    public bool EngyAttack;

    [Header("Camera Controllser")]
    [SerializeField] float rotationSpeed;
    [HideInInspector] Camera playerCamera;


    [Header("References")]
    //[HideInInspector] public PlayerInput playerInput;
    public PlayerInputs playerInputs;
    [HideInInspector] private CharacterController characterController;
    [HideInInspector] public Animator playerAnimator;


}
