using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    [Header("Movement Data")]
    [SerializeField] Vector3 moveVector;
    [SerializeField] Vector2 inputVector;



    [Header("Character Movement")]
    [SerializeField] Transform followTarget;
    [SerializeField] float moveSpeed;
    [SerializeField] float playerRotation;
    [SerializeField] Vector3 gravity;


    [Header("Camera Controller")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float rotationSpeed;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public CharacterController characterController;


}
