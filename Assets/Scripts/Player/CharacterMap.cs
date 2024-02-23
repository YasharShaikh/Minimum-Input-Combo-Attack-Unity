using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    private void OnMovement(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
        moveVector.x = inputVector.x;
        moveVector.z = inputVector.y;

    }


    private void OnCameraMove(InputValue inputValue)
    {
        mouseInputVector = inputValue.Get<Vector2>();
        mouseVector.x = mouseInputVector.x;
        mouseVector.y = mouseInputVector.y;


        Debug.Log("X = " + mouseVector.x);
        Debug.Log("Y = " + mouseVector.y);
    }


}
