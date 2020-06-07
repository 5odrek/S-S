using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBullet : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float detectionDistance;

    public Vector3 direction;
    Vector3 prevPos;
    Vector3 motion;

    GameObject mySprite;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        prevPos = transform.position;
        transform.Translate(direction * Time.deltaTime * moveSpeed);

        DetectCollision();
        motion = transform.position - prevPos;
      
    }

    private void DetectCollision()
    {
        Ray2D ray = new Ray2D(transform.position, motion);
        Debug.DrawRay(ray.origin, ray.direction);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, detectionDistance, collisionMask);

        if (hit)
        {
            if (hit.transform.tag == "Shield")
            {

                Debug.Log("hit shield");
                direction = Vector3.Reflect(motion.normalized, hit.normal);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                mySprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

  
}
