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
        if (context.action.WasPressedThisFrame())
        {
            leftFire = true;
        }
    }
    public void OnRightFire(InputAction.CallbackContext context)
    {
        if(context.action.WasPressedThisFrame())
        {
            rightFire = true;
        }
    }

    private void Update()
    {
        Debug.Log(leftFire);
        Debug.Log(rightFire);
    }
}
