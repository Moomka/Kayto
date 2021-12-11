using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] Vector2 riverDirection = new Vector2(0, 1.2f);
    public Vector2 raftDirection;
    private Vector2 userInput;
    private Vector2 windDirection;
    private GameSettings.playerStates playerState;

    private float xAxis; 
    private float yAxis; 
    [SerializeField] private float _tiltRaftZ = 10;
    [SerializeField] private float _tiltRaftX = 10;


    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        if (yAxis < 0) yAxis = 0;
        userInput = new Vector2(xAxis, yAxis);
        if (!GameSettings.gameOnPause)
        {
            UpdatePlayerState();
            MoveRaft();
        }
    }

    private void MoveRaft()
    {
        raftDirection = GameSettings.playerState == GameSettings.playerStates.raftControl ? (windDirection + riverDirection + userInput) : (windDirection + riverDirection);
        this.gameObject.transform.Translate(new Vector3(raftDirection.x, 0f, raftDirection.y) * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Euler(-yAxis*_tiltRaftX, xAxis*_tiltRaftZ, 0);
    }

    private void  UpdatePlayerState()
    {
        if (Input.GetKeyDown(attackKey))
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
