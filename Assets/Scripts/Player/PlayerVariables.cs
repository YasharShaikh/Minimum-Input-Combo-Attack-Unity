using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    [Header("Movement Data")]
    public Vector3 moveVector;
    public Vector2 inputVector;


    [Header("Look Around")]
    public float mouseSensitivity;
    public Vector3 mouseVector;
    public Vector2 mouseInputVector;


    [Header("Character Movement")]
    public float moveSpeed;
    public float playerRotation;
    public Vector3 gravity;



    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public CharacterController characterController;


}
