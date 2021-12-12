using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum DirectionWind
{
    none,
    right,
    left,
    front,
    back,
    right_front,
    left_front,
    right_back,
    left_back
}

public class FlagWind : MonoBehaviour
{
    //[SerializeField] private GameObject raft;
    [SerializeField] private int timeChangeWind;
    public float speed = 0.5f;
    private Vector3 _dirV;
    public DirectionWind dirWind = DirectionWind.none;

    void Start()
    {
        Invoke("ChangeOfWind", 10);
    }
    void Update()
    {
        switch (dirWind)
        {
            case DirectionWind.back:
                _dirV = new Vector3(0,0,-1);
                break;
            case DirectionWind.front:
                _dirV = new Vector3(0,0,1);
                break;
            case DirectionWind.left:
                _dirV = new Vector3(-1,0, 0);
                break;
            case DirectionWind.right:
                _dirV = new Vector3(1,0, 0);
                break;
            case DirectionWind.right_front:
                _dirV = new Vector3(1,0, 1);
                break;
            case DirectionWind.left_front:
                _dirV = new Vector3(-1,0, 1);
                break;
            case DirectionWind.right_back:
                _dirV = new Vector3(1,0, -1);
                break;
            case DirectionWind.left_back:
                _dirV = new Vector3(-1,0, -1);
                break;
            default:
                _dirV = new Vector3(0, 0, 0);
                return;
        }
        Rotation();
       
    }

    void ChangeOfWind()
    {
        int rn = UnityEngine.Random.Range(1, 7);
        dirWind = (DirectionWind) rn;
        Invoke("ChangeOfWind", timeChangeWind);
    }

    public Vector3 TakeDirectionWind()
    {
        return _dirV.normalized;
    }

    public void Rotation()
    {
        if (_dirV != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dirV), Time.deltaTime * speed);
        }
    }
}
