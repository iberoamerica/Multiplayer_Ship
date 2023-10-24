using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    public Vector2 movementInput;
    public bool leftFire;
    public bool rightFire;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnLeftFire(InputAction.CallbackContext context)
    {
     
        if (context.started)
        {
            leftFire = true;
            print("teste1");
        }else if (context.performed)
        {
            print("teste2");
            leftFire = false;
        }
    }
    public void OnRightFire(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            rightFire = true;
        }else if (context.performed)
        {
            rightFire = false;
        }
    }
}
