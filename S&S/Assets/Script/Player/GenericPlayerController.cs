using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayerController : MonoBehaviour
{

    protected Vector2 moveDir;

    private Grid myGrid;

    [SerializeField] protected int life;

    private Node myNode;

    private RaycastLogic myRaycastLogic;

    [SerializeField] private LayerMask levelMask;

    [SerializeField] private float recoilLerpFactor;
    [SerializeField] private float currentRecoilSpeed;
    [SerializeField] private float baseRecoilSpeed;
    [SerializeField] private float recoilDistance;

    [SerializeField] protected float modifiedSpeed;
    [SerializeField] protected float currentSpeed;
    [SerializeField] protected float baseMoveSpeed;

    protected enum PlayerState { Idle, Moving, InAction, OnRecoil}
    [SerializeField] protected PlayerState myState;

    protected enum PlayerDirection { North, South, East, West, NorthWest, NorthEast, SouthWest, SouthEast }
    [SerializeField] protected PlayerDirection myDir;

    
    private void Start()
    {
        currentSpeed = baseMoveSpeed;
        myState = PlayerState.Idle;
        myRaycastLogic = transform.GetChild(1).GetComponent<RaycastLogic>();
       
    }


    private void Update()
    {
        if(myGrid != null)
        {
            myNode = myGrid.NodeFromWorldPoint(transform.position);
        }
 

        Vector2 move = new Vector2(moveDir.x, moveDir.y) * currentSpeed * Time.deltaTime;

        myRaycastLogic.UpdateRaycastOrigins();

        if (move.x != 0)
        {
            myRaycastLogic.HorizontalCollision(ref move);
        }

        if (move.y != 0)
        {
            myRaycastLogic.VerticalCollision(ref move);
        }
        
        if (move != Vector2.zero && myState == PlayerState.Idle)
        {
            transform.Translate(move, Space.World);
        }

        if (myState != PlayerState.InAction)
        {
            DetermineDir(moveDir);
        }
    }

    private void DetermineDir(Vector2 stickDirection)
    {
        if (stickDirection.x >= 0.5f && stickDirection.y <= 0.5f && stickDirection.y >= -0.5f)
        {
            myDir = PlayerDirection.East;
        }
        else if (stickDirection.x >= 0.5f && stickDirection.y >= 0.5f)
        {
            myDir = PlayerDirection.NorthEast;
        }
        else if (stickDirection.x >= 0.5f && stickDirection.y <= -0.5f)
        {
            myDir = PlayerDirection.SouthEast;
        }
        else if (stickDirection.x <= -0.5f && stickDirection.y <= 0.5f && stickDirection.y >= -0.5f)
        {
            myDir = PlayerDirection.West;
        }
        else if (stickDirection.x <= -0.5f && stickDirection.y >= 0.5f)
        {
            myDir = PlayerDirection.NorthWest;
        }
        else if (stickDirection.x <= -0.5f && stickDirection.y <= -0.5f)
        {
            myDir = PlayerDirection.SouthWest;
        }
        else if (stickDirection.y >= 0.5f && stickDirection.x <= 0.5f && stickDirection.x >= -0.5f)
        {
            myDir = PlayerDirection.North;
        }
        else if (stickDirection.y <= -0.5f && stickDirection.x <= 0.5f && stickDirection.x >= -0.5f)
        {
            myDir = PlayerDirection.South;
        }
    }


    public void TakeHit(int damage, Vector3 impact)
    {
        life -= damage;

        myState = PlayerState.OnRecoil;
        StartCoroutine(Recoil(impact));
        
    }

    private IEnumerator Recoil(Vector3 hitImpact)
    {
        Vector3 startPos = transform.position;
        currentRecoilSpeed = baseRecoilSpeed;
        Vector3 recoilDir = (transform.position - hitImpact + new Vector3(0, 0.5f, 0)).normalized;
        Debug.DrawLine(transform.position, recoilDir);

        while (myState == PlayerState.OnRecoil)
        {
            transform.position += recoilDir * currentRecoilSpeed * Time.deltaTime;
            currentRecoilSpeed = Mathf.Lerp(currentRecoilSpeed, 0, recoilLerpFactor);

            if (Vector3.Distance(startPos, transform.position) > recoilDistance || currentRecoilSpeed <= 0.01f)
            {
                myState = PlayerState.Idle;
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if(myNode != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(myNode.worldPosition, Vector3.one / 5);
        }
        
    }
}
