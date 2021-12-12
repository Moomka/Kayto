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

    private void Update()
    {
        if (currentPointIndex >= points.Length) currentPointIndex = 0;
        gameObject.transform.LookAt(currentTargetPoint);
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (Vector3.Distance(gameObject.transform.position, currentTargetPoint.position) < minimalDistance)
        {
            currentPointIndex++;
        }
    }
}
