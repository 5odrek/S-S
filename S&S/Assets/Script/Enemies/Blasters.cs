using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blasters : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    [SerializeField] float moveSpeed;

    [SerializeField] float detectionRadius;

    [SerializeField] float shootCooldown;

    [SerializeField] LayerMask playerMask;

    private Transform target;

    bool shooting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            DetectTarget();
        }
        else if(!shooting)
        {
            Debug.Log(target.position);
            shooting = true;
            StartCoroutine("ShootRoutine");
        }
    }

    private void DetectTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerMask);

        if (targets.Length > 0)
        {
            target = targets[0].transform;
        }
    }

    private void Shoot()
    {
       
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = transform.position;
        newBullet.transform.parent = transform;
        GenericBullet bulletScript = newBullet.GetComponent<GenericBullet>();
        bulletScript.direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(bulletScript.direction.y, bulletScript.direction.x) * Mathf.Rad2Deg;
        newBullet.transform.GetChild(0).transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }

    IEnumerator ShootRoutine()
    {
        while (shooting)
        {
            yield return new WaitForSeconds(shootCooldown);
            Shoot();
        }
     
    }
}
