using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;
    [SerializeField] float speed;
    [SerializeField] float sensitivity;
    float distanceTravelled;
    private bool moving = false;
    private int currentSegment = 0;
    private Vector3 goalPoint;
    private Vector3 lastGoalPoint;
    [SerializeField] Transform target;
    private float pathLength;
    // Start is called before the first frame update

    void Start()
    {
        transform.position = pathCreator.path.GetPointAtDistance(0);
        if (target != null) transform.LookAt(target);
        else { transform.rotation = pathCreator.path.GetRotationAtDistance(0); }

        pathLength = pathCreator.path.length;
        Debug.Log("PathLength: " + pathLength);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        //goalPoint = pathCreator.bezierPath.GetPointsInSegment(currentSegment)[0];
        lastGoalPoint = goalPoint;
        goalPoint = pathCreator.path.GetClosestPointOnPath(target.position);
        if (Vector3.Distance(goalPoint, lastGoalPoint) < sensitivity) goalPoint = lastGoalPoint;
        FollowPlayer(goalPoint,2f);
    }

    void FollowPlayer(Vector3 targetPosition, float travelTime)
    {
        float time = 0f;
        currentSegment++;
        if (currentSegment % pathCreator.bezierPath.NumAnchorPoints == 0) currentSegment = 0;
        Vector3 startPosition = transform.position;

        if (Vector3.Distance(transform.position, goalPoint) >= 0.1)
        {
            Vector3 pointRight, pointLeft;
            float distanceRight, distanceLeft;
            distanceRight = distanceTravelled + speed * Time.deltaTime;
            pointRight = pathCreator.path.GetPointAtDistance(distanceRight);
            distanceLeft = distanceTravelled - speed * Time.deltaTime;
            pointLeft = pathCreator.path.GetPointAtDistance(distanceLeft);

            transform.position = (Vector3.Distance(pointLeft, goalPoint) < Vector3.Distance(pointRight, goalPoint)) ? pointLeft : pointRight;
            if (target != null) transform.LookAt(target);
            else { transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled); }

            distanceTravelled = (Vector3.Distance(pointLeft, goalPoint) < Vector3.Distance(pointRight, goalPoint)) ? distanceLeft : distanceRight;

            float t = time / travelTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f); //Smootherstep
            //transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        }
        
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float travelTime)
    {
        float time = 0f;
        Vector3 startPosition = transform.position;

        while (Vector3.Distance(transform.position, goalPoint) >= 0.1)
        {
            float t = time / travelTime;
            //t = t * t * (3f - 2f * t); //Smoothstep
            t = t * t * t * (t * (6f * t - 15f) + 10f); //Smootherstep
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.LookAt(target);
            time += Time.deltaTime;
            Debug.Log("DENTRO DEL WHILE");
            yield return null;
        }

    }
}
