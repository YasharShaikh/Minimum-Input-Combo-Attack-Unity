using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public partial class PlayerStateManager
{

    [Header("Movement Data")]



    [Header("Character Movement")]
    [SerializeField] Transform followTarget;
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float playerRotation;
    [Space]
    [SerializeField] Vector3 gravity;
    [Header("Player animations")]
    [SerializeField] int VelocityHash;
    [Space]
    public bool SwdAttack;
    public bool EngyAttack;

    [Header("Camera Controllser")]
    [SerializeField] float rotationSpeed;
    [HideInInspector] Camera playerCamera;


    [Header("References")]
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] private CharacterController characterController;
    [HideInInspector] Animator playerAnimator;


}
