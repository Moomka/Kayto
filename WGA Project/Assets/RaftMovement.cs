using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftMovement : MonoBehaviour
{
    [SerializeField] Vector2 riverDirection = new Vector2(0, 1);
    public Vector2 raftDirection;
    private Vector2 userInput;
    private Vector2 windDirection;
    

    private void Update()
    {
        userInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        raftDirection = (userInput + windDirection + riverDirection) * Time.deltaTime;
        this.gameObject.transform.Translate(new Vector3(raftDirection.x, 0f, raftDirection.y));
    }
}
