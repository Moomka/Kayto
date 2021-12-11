using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    public Vector2 raftDirection;

    [SerializeField] Vector2 riverDirection = new Vector2(0, 1.2f);
    [SerializeField] private float _tiltRaftZ = 10f;
    [SerializeField] private float _tiltRaftX = 10f;
    [SerializeField] private float _raftIncline = 2f;
    [SerializeField] private float _raftRotationSpeed;
    [SerializeField] private float _raftRotateBorder;

    private Quaternion raftRotation;
    private Vector2 userInput;
    private Vector2 windDirection;
    private GameSettings.playerStates playerState;
    private float xAxis; 
    private float yAxis; 

    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        if (yAxis < 0) yAxis = 0;
        userInput = new Vector2(xAxis, yAxis);
        raftRotation = Quaternion.Euler(0f, Mathf.Clamp(xAxis * _raftRotationSpeed, -_raftRotateBorder, _raftRotateBorder), 0f);
        if (!GameSettings.gameOnPause)
        {
            UpdatePlayerState();
            MoveRaft();
        }
    }

    private void MoveRaft() 
    { 
        if (GameSettings.playerState == GameSettings.playerStates.raftControl)
        {
            raftDirection = (windDirection + riverDirection + userInput) * Time.deltaTime;
            if (xAxis != 0)
            {
                transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, raftRotation, _raftRotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, 0f, 0f), _raftRotationSpeed * Time.deltaTime);
            }
        }
        else 
        {
            raftDirection = (windDirection + riverDirection) * Time.deltaTime;
        }
        this.gameObject.transform.Translate(new Vector3(raftDirection.x, 0f, raftDirection.y), Space.World);
    }

    private void  UpdatePlayerState()
    {
        if (Input.GetMouseButton(0))
        {
            playerState = GameSettings.playerStates.attack;
            GameSettings.playerState = playerState;
        }
        else
        {
            playerState = GameSettings.playerStates.raftControl;
            GameSettings.playerState = playerState;
        }
    }
}
