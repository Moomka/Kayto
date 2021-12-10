using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] Vector2 riverDirection = new Vector2(0, 1);
    public Vector2 raftDirection;
    private Vector2 userInput;
    private Vector2 windDirection;
    private GameSettings.playerStates playerState;


    private void Update()
    {
        userInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        this.transform.Rotate(Vector3.up, raftDirection.x * Time.deltaTime * 10);
        Debug.Log("тест коммита");
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
