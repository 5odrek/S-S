using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ShieldController : GenericPlayerController
{

    private ShieldInputs myControls;

    private GameObject shield;

    private InputUser user;

    

    private void Awake()
    {
        myControls = new ShieldInputs();
        user = InputUser.PerformPairingWithDevice(Gamepad.all[1]);
        user.AssociateActionsWithUser(myControls);

        shield = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        myControls.ShieldActions.Move.performed += ctx => moveDir = ctx.ReadValue<Vector2>();
        myControls.ShieldActions.RaiseShield.performed += ctx => RaiseShield();
        myControls.ShieldActions.RaiseShield.canceled += ctx => LowerShield();
        myControls.ShieldActions.Move.canceled += ctx => moveDir = Vector2.zero;
        myControls.ShieldActions.Move.Enable();
        myControls.ShieldActions.RaiseShield.Enable();
    }
    
    private void LowerShield()
    {
        myState = PlayerState.Idle;
        shield.transform.rotation = new Quaternion(0, 0, 0, 0);
        shield.SetActive(false);
        currentSpeed = baseMoveSpeed;
    }

    private void RaiseShield()
    {
        myState = PlayerState.InAction;
        float rotValue = 0;

        currentSpeed = modifiedSpeed;

        switch (myDir)
        {
            case PlayerDirection.North:
                rotValue = 90;
                break;
            case PlayerDirection.NorthEast:
                rotValue = 45;
                break;
            case PlayerDirection.NorthWest:
                rotValue = 135;
                break;
            case PlayerDirection.South:
                rotValue = 270;
                break;
            case PlayerDirection.SouthEast:
                rotValue = 315;
                break;
            case PlayerDirection.SouthWest:
                rotValue = 225;
                break;
            case PlayerDirection.East:
                rotValue = 0;
                break;
            case PlayerDirection.West:
                rotValue = 180;
                break;
        }

        shield.transform.Rotate(0, 0, rotValue, Space.World);
        shield.SetActive(true);
    }
    
}
