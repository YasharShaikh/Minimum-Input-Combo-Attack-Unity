using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    private void OnMovement(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
        moveVector.x = inputVector.x;
        moveVector.z = inputVector.y;

        Debug.Log("X = " + moveVector.x);
        Debug.Log("Y = " + moveVector.y);
    }
}
