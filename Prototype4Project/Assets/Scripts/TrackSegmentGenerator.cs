using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegmentGenerator : MonoBehaviour
{
    public GameObject trackSegmentPrefab;
    public Transform startPoint;
    public Transform endPoint;
    public float maxTurnAngle = 30f; // Maximum turn angle in degrees

    private void Start()
    {
        //GenerateStraightTrack();
        GenerateCurvedTrack();
    }

    public float segmentLength = .2f; // Length of each track segment
    public bool createInStartPoint = true; // Whether to create a segment at the start point

    public void GenerateStraightTrack()
    {
        // Calculate the direction and length of the track
        Vector3 trackDirection = (endPoint.position - startPoint.position).normalized;
        float totalTrackLength = Vector3.Distance(startPoint.position, endPoint.position);

        // Calculate the rotation to align with the track direction
        Quaternion rotation = Quaternion.LookRotation(trackDirection);

        // Calculate the number of segments needed
        int numSegments = Mathf.CeilToInt(totalTrackLength / segmentLength);

        // Offset to evenly distribute segments along the track
        Vector3 offset = trackDirection * segmentLength;

        // Start point for segment creation
        Vector3 currentPoint = startPoint.position;

        if (createInStartPoint)
        {
            Instantiate(trackSegmentPrefab, currentPoint, rotation);
        }

        for (int i = 0; i < numSegments - 1; i++)
        {
            // Calculate the position of the next segment
            currentPoint += offset;
            Instantiate(trackSegmentPrefab, currentPoint, rotation);
        }
    }

    public void GenerateCurvedTrack()
    {
        // Calculate the direction and length of the track
        Vector3 trackDirection = (endPoint.position - startPoint.position).normalized;
        float totalTrackLength = Vector3.Distance(startPoint.position, endPoint.position);

        // Calculate the curve radius based on the maximum turn angle
        float curveRadius = totalTrackLength; // (2 * Mathf.Sin(Mathf.Deg2Rad * (maxTurnAngle / 2)));

        // Calculate the number of segments needed
        int numSegments = Mathf.CeilToInt(totalTrackLength / segmentLength);

        // Calculate the angular step for each segment
        float angularStep = maxTurnAngle / numSegments;

        // Start point for segment creation
        Vector3 currentPoint = startPoint.position;
        Quaternion rotation = Quaternion.LookRotation(trackDirection);

        for (int i = 0; i < numSegments; i++)
        {
            // Calculate the position of the next segment along the curve
            float angle = angularStep * i;
            Vector3 position = startPoint.position + (Quaternion.Euler(0, angle, 0) * (trackDirection * curveRadius));

            // Calculate the rotation to align the track segment
            Quaternion segmentRotation = rotation * Quaternion.Euler(0, angle-90, 0);

            // Instantiate the track segment
            Instantiate(trackSegmentPrefab, position, segmentRotation);
        }
    }


    public void GenerateTrackSegment()
    {
        float distanceAB = Vector3.Distance(startPoint.position, endPoint.position);
        float curveRadius = distanceAB / (2 * Mathf.Sin(Mathf.Deg2Rad * (maxTurnAngle / 2)));

        // Calculate the center of the circle
        Vector3 centerPoint = (startPoint.position + endPoint.position) / 2f;

        // Calculate the number of waypoints (more waypoints for smoother curves)
        int numWaypoints = 10;
        for (int i = 0; i < numWaypoints; i++)
        {
            float angle = i * (maxTurnAngle / (numWaypoints - 1)) - (maxTurnAngle / 2);
            float x = curveRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
            float z = curveRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            Vector3 waypointPosition = centerPoint + new Vector3(x, 0, z);
            Instantiate(trackSegmentPrefab, waypointPosition, Quaternion.identity);
        }
    }
}
