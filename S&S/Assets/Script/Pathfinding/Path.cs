using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path 
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] TurnBoundaries;
    public readonly int finishLineIndex;

    public Path(Vector3[] waypoints, Vector3 startPos, float turndist)
    {
        lookPoints = waypoints;
        TurnBoundaries = new Line[lookPoints.Length];
        finishLineIndex = TurnBoundaries.Length - 1;

        Vector2 previousPoint = V3ToV2(startPos);

        for (int i = 0; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3ToV2(lookPoints[i]);
            Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundaryPoint = (i == finishLineIndex)? currentPoint : currentPoint - dirToCurrentPoint * turndist;

            TurnBoundaries[i] = new Line(turnBoundaryPoint, previousPoint - dirToCurrentPoint * turndist);
            previousPoint = turnBoundaryPoint;
        }
    }

    Vector2 V3ToV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;

        foreach(Vector3 p in lookPoints)
        {
            Gizmos.color = (p == lookPoints[lookPoints.Length - 1]) ? Color.yellow : Color.black;
            Gizmos.DrawCube(p, Vector3.one / 5);
        }

        Gizmos.color = Color.white;

        foreach (Line l in TurnBoundaries)
        {
            l.DrawWithGizmos(10);
        }
    }
}
