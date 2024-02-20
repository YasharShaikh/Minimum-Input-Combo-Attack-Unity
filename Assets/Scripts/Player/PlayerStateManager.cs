using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager : MonoBehaviour
{
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();  
        playerInput = GetComponent<PlayerInput>();
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

    #region movement

    public void ApplyGravity()
    {
        characterController.Move(gravity*Time.deltaTime);
    }
    public void Move()
    {
        characterController.Move(moveVector * moveSpeed * Time.deltaTime);
    }
    #endregion
}
