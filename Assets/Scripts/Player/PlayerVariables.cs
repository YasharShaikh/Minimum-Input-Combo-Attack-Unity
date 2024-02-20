using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    [Header("Movement Data")]
    public Vector3 moveVector;
    public Vector2 inputVector;

    [Header("Character Movement")]
    public float moveSpeed;
    public float playerRotation;
    public Vector3 gravity;


    public PlayerInput playerInput;
    public CharacterController characterController;

   
}
