using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] Vector2 moveinputs;


    CharacterController characterController;
    PlayerInputs playerInputs;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();







    }
    private void OnEnable()
    {

        if(playerInputs == null)
        {
            playerInputs = new PlayerInputs();
            playerInputs.Player.Move.performed += i => moveinputs = i.ReadValue<Vector2>();
        }
        playerInputs.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
