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
    back
}

public class FlagWind : MonoBehaviour
{
    //[SerializeField] private GameObject raft;
    public float speed = 0.5f;
    private Vector3 _dirV;
    public DirectionWind dirWind = DirectionWind.none;
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
            default:
                _dirV = new Vector3(0, 0, 0);
                return;
        }
        Rotation();
       
    }
    

    public void Rotation()
    {
        if (_dirV != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dirV), Time.deltaTime * speed);
        }
        
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x);
    }
}
