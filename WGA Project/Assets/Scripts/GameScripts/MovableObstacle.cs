using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    [SerializeField] float speed;
    [SerializeField] bool slowOnPoints;
    [SerializeField] float minimalDistance;
    Transform currentTargetPoint;
    int currentPointIndex = 0;

    private void Awake()
    {
        currentTargetPoint = points[0].transform;
    }
    private void Update()
    {
        if (currentPointIndex >= points.Length) currentPointIndex = 0;
        currentTargetPoint = points[currentPointIndex].transform;
        Vector3 direction = Vector3.Normalize(currentTargetPoint.position - gameObject.transform.position);
        gameObject.transform.Translate(direction * Time.deltaTime * speed);

        if (Vector3.Distance(gameObject.transform.position, currentTargetPoint.position) < minimalDistance)
        {
            currentPointIndex++;
        }
    }
}
