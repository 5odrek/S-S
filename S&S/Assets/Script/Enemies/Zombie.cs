using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : GenericEnemy
{
    [Header("Charge Attack Parameters")]
    [SerializeField] float chargeRadius;
    [SerializeField] float chargeChannel;
    [SerializeField] float chargeDistance;
    [SerializeField] float chargeSpeed;
    [SerializeField] float baseRecoilSpeed;
    [SerializeField] float chargeCooldown;
    [SerializeField] float recoilDistance;
    [Space(10)]

    [Header("Debug Only")]
    [SerializeField] private ZombiState myState;
    [Space(10)]

    [Header("Detection")]
    [SerializeField] LayerMask impactMask;
    [SerializeField] float impactDistanceDetection;
    [Space(10)]

    private float currentRecoilSpeed;

    private enum ZombiState { Idle, Moving, Chasing, Locking, Buffering, Charging, OnRecoil, OnCooldown };

    private Vector3 chargeDir;

    private void Start()
    {
        myState = ZombiState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case ZombiState.Moving:
                DetectTargetsInRange();
                UpdateTargetsInRange();
                DefineCurrentTarget();

                if (currentTarget != null)
                {
                    StartCoroutine(UpdatePath());
                    myState = ZombiState.Chasing;
                }

                break;

            case ZombiState.Idle:
                DetectTargetsInRange();
                UpdateTargetsInRange();
                DefineCurrentTarget();

                if (currentTarget != null)
                {
                    StartCoroutine(UpdatePath());
                    myState = ZombiState.Chasing;
                }
                else
                {
                    myState = ZombiState.Moving;
                    StartCoroutine("MoveCycle");
                }

                break;
            case ZombiState.Chasing:
                DetectTargetsInRange();
                UpdateTargetsInRange();
                DefineCurrentTarget();

                if (currentTarget != null)
                {
                    CheckDistanceToCurrentTarget();
                }
                
                break;
            case ZombiState.Locking:
                chargeDir = (currentTarget.position - transform.position + new Vector3(0,aimVerticalOffset,0)).normalized;
                break;
        }

    }


    private void CheckDistanceToCurrentTarget()
    {
        if (Vector3.Distance(transform.position, currentTarget.position) < chargeRadius)
        {
            
            myState = ZombiState.Locking;
            canMove = false;
            followingPath = false;
            StopPath();
            StartCoroutine("LockTarget");
        }
    }

    IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(chargeCooldown);
        myState = ZombiState.Idle;
        canMove = true;
    }


    IEnumerator MoveCycle()
    {
        while (currentTarget == null)
        {
            Debug.Log("In random cycle");
            StopPath();
            MoveRandom();
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator LockTarget()
    {
        yield return new WaitForSeconds(chargeChannel / 2);
        myState = ZombiState.Buffering;
        yield return new WaitForSeconds(chargeChannel / 2);
        
        StartCoroutine("Charge");
    }

    private IEnumerator Charge()
    {
        myState = ZombiState.Charging;
        Vector3 startPos = transform.position;

        while (myState == ZombiState.Charging)
        {
            transform.position += chargeDir * chargeSpeed * Time.deltaTime;
            DetectImpact();

            if (Vector3.Distance(startPos, transform.position) >= chargeDistance)
            {
                myState = ZombiState.OnCooldown;
                StartCoroutine("Reactivate");
            }
            yield return null;
            currentTarget = null;
        }
    }

    private void DetectImpact()
    {
        Ray ray = new Ray(transform.position, chargeDir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, chargeDir, impactDistanceDetection, impactMask);

        if (hit)
        {
            
            if (hit.transform.tag == "Shield" || hit.transform.tag == "Player")
            {
                myState = ZombiState.OnRecoil;
                StartCoroutine(Recoil(hit.normal));

                if(hit.transform.tag == "Player")
                {
                    GenericPlayerController playerHit = hit.transform.gameObject.GetComponent<GenericPlayerController>();
                    playerHit.TakeHit(1, transform.position);
                }
            }
            else
            {
                myState = ZombiState.OnCooldown;
                StartCoroutine("Reactivate");
            }
        }
    }

   IEnumerator Recoil(Vector3 hitNormal)
    {
        Vector3 startPos = transform.position;
        currentRecoilSpeed = baseRecoilSpeed;
        Vector3 recoilDir = Vector3.Reflect(chargeDir, hitNormal);

        while (myState == ZombiState.OnRecoil)
        {
            transform.position += recoilDir * currentRecoilSpeed * Time.deltaTime;
            currentRecoilSpeed = Mathf.Lerp(currentRecoilSpeed, 0, 0.2f);

            if (Vector3.Distance(startPos, transform.position) > recoilDistance || currentRecoilSpeed <= 0.01f)
            {
                myState = ZombiState.OnCooldown;
                StartCoroutine("Reactivate");
            }
            yield return null;
        }
    }



   


}
