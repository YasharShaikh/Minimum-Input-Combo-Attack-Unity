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
    private void FixedUpdate()
    {
        CameraRotation();
    }
    #region movement

    public void ApplyGravity()
    {
        characterController.Move(gravity*Time.deltaTime);
    }
    public void Move()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, mouseVector, Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(moveVector * Time.deltaTime);
        characterController.Move(moveVector * moveSpeed * Time.deltaTime);
    }

    public void CameraRotation()
    {
        followTarget.rotation =Quaternion.identity;
    }
    #endregion
}
