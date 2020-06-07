using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;


public class SwordController : GenericPlayerController
{
    
    private SwordInputs myControls;

    private GameObject sword;
    private GameObject debugArrow;

    private InputUser user;

    [SerializeField] private float strikeDuration;

    private void Awake()
    {
        myControls = new SwordInputs();
        user = InputUser.PerformPairingWithDevice(Gamepad.all[0]);
        user.AssociateActionsWithUser(myControls);

        sword = transform.GetChild(0).gameObject;
        debugArrow = transform.GetChild(2).gameObject;
    }

    private void OnEnable()
    {
        myControls.SwordActions.Move.performed += ctx => moveDir = ctx.ReadValue<Vector2>();
        myControls.SwordActions.Move.canceled += ctx => moveDir = Vector2.zero;
        myControls.SwordActions.Strike.performed += ctx => PrepareStrike();
        myControls.SwordActions.Strike.canceled += ctx => Strike();
        myControls.SwordActions.Move.Enable();
        myControls.SwordActions.Strike.Enable();
    }

    private void PrepareStrike()
    {
        currentSpeed = modifiedSpeed;
        myState = PlayerState.InAction;
        debugArrow.transform.Rotate(0, 0, CalculateStrikeAngle(), Space.World);
        debugArrow.SetActive(true);
    }

    private float CalculateStrikeAngle()
    {
        debugArrow.SetActive(false);
        debugArrow.transform.rotation = new Quaternion(0, 0, 0, 0);
        float rotValue = 0;

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
        return rotValue;
    }

    private void Strike()
    {
        sword.transform.Rotate(0, 0, CalculateStrikeAngle(), Space.World);
        
        sword.SetActive(true);
        StartCoroutine("StrikeRoutine");
    }

        IEnumerator StrikeRoutine()
    {
        yield return new WaitForSeconds(strikeDuration);
        sword.SetActive(false);
        myState = PlayerState.Idle;
        currentSpeed = baseMoveSpeed;
        sword.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}

