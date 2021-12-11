using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    public Vector2 raftDirection;

    [SerializeField] Vector2 _riverDirection = new Vector2(0, 1.2f);
    [SerializeField] private float _raftRotationSpeed;
    [SerializeField] private float _raftRotateBorder;
    [SerializeField] private float _raftTilt;

    private Quaternion _raftRotation;
    private Vector2 _userInput;
    private Vector2 _windDirection;
    private GameSettings.playerStates _playerState;
    private float _xAxis; 
    private float _yAxis;

    private void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");
        if (_yAxis < 0) _yAxis = 0;
        _userInput = new Vector2(_xAxis, _yAxis);
        _raftRotation = Quaternion.Euler(-_yAxis * 2f, _xAxis * _raftRotateBorder, _xAxis * _raftTilt);
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
            raftDirection = (_windDirection + _riverDirection + _userInput) * Time.deltaTime;
            if (_userInput != Vector2.zero)
            {
                transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, _raftRotation, _raftRotationSpeed * Time.deltaTime);
            } }
        else
        {
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, 0f, 0f), _raftRotationSpeed * Time.deltaTime);
            raftDirection = (_windDirection + _riverDirection) * Time.deltaTime;
        }
        this.gameObject.transform.Translate(new Vector3(raftDirection.x, 0f, raftDirection.y), Space.World);
    }

    private void  UpdatePlayerState()
    {
        if (Input.GetMouseButton(0))
        {
            _playerState = GameSettings.playerStates.attack;
            GameSettings.playerState = _playerState;
        }
        else
        {
            _playerState = GameSettings.playerStates.raftControl;
            GameSettings.playerState = _playerState;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Vector3 hitPoint = collision.collider.ClosestPoint(gameObject.transform.position);
            Vector3 backPush = transform.position - hitPoint;
            backPush.y = 0;
            gameObject.transform.Translate(backPush, Space.World);
        }
    }
}
