using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{

    [Header("G_Pathfinding")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnDist;
    [Space(10)]

    [Header("G_Debug Only")]
    [SerializeField] protected Transform currentTarget;
    [SerializeField] protected List<Transform> targetsInRange;
    [Space(10)]

    [Header("G_Detection")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float detectionRadius;
    [Space(10)]

    [Header("G_Gameplay")]
    [SerializeField] private float speed;
    [SerializeField] protected float aimVerticalOffset;
    [Space(10)]

    private const float minPathUpdateTime = .01f;
    private const float pathUpdateMoveTreshold = 0.1f;

    private Grid myGrid;

    protected bool canMove = true;
    protected bool followingPath;

    private Path path;

    private void Awake()
    {
        myGrid = FindObjectOfType<Grid>();
    }

    private void Start()
    {
        targetsInRange = new List<Transform>();
    }


    protected void DetectTargetsInRange()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerMask);

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (!targetsInRange.Contains(targets[i].transform))
                {
                    targetsInRange.Add(targets[i].transform);
                }
                
            }
        }
    }

    protected void UpdateTargetsInRange()
    {
        for (int i = 0; i < targetsInRange.Count; i++)
        {
            if (Vector3.Distance(transform.position, targetsInRange[i].position) > detectionRadius)
            {
                targetsInRange.Remove(targetsInRange[i]);
            }
        }
        
    }

    protected void DefineCurrentTarget()
    {
        float distanceToTarget = 100;

        if (targetsInRange.Count == 1)
        {
            currentTarget = targetsInRange[0];
        }
        else if (targetsInRange.Count > 1)
        {
            for (int i = 0; i < targetsInRange.Count; i++)
            {
                if (Vector3.Distance(targetsInRange[i].position, transform.position) < distanceToTarget)
                {
                    currentTarget = targetsInRange[i];
                    distanceToTarget = Vector3.Distance(targetsInRange[i].position, transform.position);
                }
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    protected void MoveRandom()
    {
        Debug.Log("MoveRandom");

        int x = Random.Range(-3, 3);
        int y = Random.Range(-3, 3);
        Vector3 nextLocation = transform.position + new Vector3(x, y, 0);


        if (myGrid.NodeFromWorldPoint(nextLocation).walkable)
        {
            PathRequestManager.RequestPath(transform.position, nextLocation, OnPathFound);
        }
        else
        {
            MoveRandom();
        }
        
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful && waypoints.Length > 0 && canMove)
        {

            path = new Path(waypoints, transform.position, turnDist);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    protected void StopPath()
    {
        StopCoroutine("FollowPath");
        StopCoroutine("UpdatePath");
    }

    protected IEnumerator UpdatePath()
    {

        if(Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(transform.position, currentTarget.position, OnPathFound);

        float sqrMoveTreshold = pathUpdateMoveTreshold * pathUpdateMoveTreshold;

        Vector3 targetPosOld = currentTarget.position;

        while (canMove)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if((currentTarget.position - targetPosOld).sqrMagnitude > sqrMoveTreshold)
            {
                PathRequestManager.RequestPath(transform.position, currentTarget.position, OnPathFound);
                targetPosOld = currentTarget.position;
            }
        }
    }

    IEnumerator FollowPath()
    {

      followingPath = true;

        int pathIndex = 0;

        Vector3 currentDir = (path.lookPoints[0] - transform.position);
        Vector3 velocity = Vector3.zero;

        while (followingPath)
        {
         
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);

            while (path.TurnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

               if (currentDir != path.lookPoints[pathIndex] - transform.position)
                {
                    currentDir = Vector3.SmoothDamp(currentDir, path.lookPoints[pathIndex] - transform.position, ref velocity, turnSpeed);
                }

                Debug.DrawLine(transform.position, path.lookPoints[0]);
                transform.position += currentDir.normalized * Time.deltaTime * speed;
            

            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}
