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
            MovePlot();
        }
    }

    private void MovePlot()
    {
        raftDirection = GameSettings.playerState == GameSettings.playerStates.raftControl ? (windDirection + riverDirection + userInput) * Time.deltaTime : (windDirection + riverDirection) * Time.deltaTime;
        this.gameObject.transform.Translate(new Vector3(raftDirection.x, 0f, raftDirection.y));
    }

    private void  UpdatePlayerState()
    {
        if (Input.GetKeyDown(attackKey))
        {
            playerState = GameSettings.playerStates.Attack;
            GameSettings.playerState = playerState;
        }
        else
        {
            playerState = GameSettings.playerStates.raftControl;
            GameSettings.playerState = playerState;
        }
    }
}
