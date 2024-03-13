using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public partial class PlayerStateManager : MonoBehaviour
{

    public static PlayerStateManager Instance;


    private void Awake()
    {

        Instance = this;
        playerInputs = new PlayerInputs();
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        playerAnimator = GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        HandleMovement();
        ApplyGravity();
    }

    private void FixedUpdate()
    {
    }

    #region movement

    #endregion
}
