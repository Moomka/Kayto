using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public bool RandomOrNot = true;
    public Transform[] moveSpots;
    private int _randomSpot;
    private float waitTime;
    public float startWaitTime;
    private int index;
    void Start()
    {
        if (RandomOrNot)
            index = UnityEngine.Random.Range(0, moveSpots.Length - 1);
        else
            index = 0;
        //_randomSpot = UnityEngine.Random.Range(0, moveSpots.Length - 1);
        waitTime = startWaitTime;
    }
    
    void Update()
    {
        MovementLogic();
    }
    public virtual void MovementLogic()
    {
        if (index != moveSpots.Length)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, moveSpots[index].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, moveSpots[index].position) < 0.01f)
            {
                if (waitTime <= 0)
                {
                    if (RandomOrNot) 
                        index = UnityEngine.Random.Range(0, moveSpots.Length - 1);
                    else
                    {
                        index++;
                        if (index == moveSpots.Length) index = 0;
                    }
                    //_randomSpot = UnityEngine.Random.Range(0, moveSpots.Length - 1);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
    
}
