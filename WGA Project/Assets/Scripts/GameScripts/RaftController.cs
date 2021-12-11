using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    public Vector2 raftDirection;
    [SerializeField] KeyCode attackKey;
    [SerializeField] Vector2 riverDirection = new Vector2(0, 1);
    [SerializeField] GameObject tongue;
    private Animation attackAnimation;
    private Vector2 userInput;
    private Vector2 windDirection;
    private GameSettings.playerStates playerState;
    private bool canSwim = true;

    private void Awake()
    {
        attackAnimation = tongue.GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackAnimation.Play();
        }
        userInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (userInput.y < 0) userInput.y = 0;
        if (!GameSettings.gameOnPause)
        {
            UpdatePlayerState();
            MoveRaft();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        canSwim = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        canSwim = true;
    }

    private void MoveRaft()
    {
        raftDirection = GameSettings.playerState == GameSettings.playerStates.raftControl ? (windDirection + riverDirection + userInput) : (windDirection + riverDirection);
        this.gameObject.transform.Translate(new Vector3(raftDirection.x, 0f, canSwim ? raftDirection.y : 0f) * Time.deltaTime, Space.World);
        this.transform.Rotate(Vector3.up, raftDirection.x * Time.deltaTime * 10);
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
