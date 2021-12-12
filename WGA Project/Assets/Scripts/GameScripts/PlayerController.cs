using System.Collections;
using System.Collections.Generic;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController S;
    public Vector2 raftDirection;

    [SerializeField] Vector2 _riverDirection = new Vector2(0, 1.2f);
    [SerializeField] private float _raftRotationSpeed;
    [SerializeField] private float _raftRotateBorder;
    [SerializeField] private float _raftTilt;
    [SerializeField] GameObject raft;
    [SerializeField] private Transform placeForUi;
    [SerializeField] float raftDamage;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image image;

    private Quaternion _raftRotation;
    private Vector2 _userInput;
    private Vector2 _windDirection;
    private GameSettings.playerStates _playerState;
    private float _xAxis;
    private float _yAxis;
    private GameObject objectView;
    private bool _isGameOver=true;

    void Start()
    {
        //if (S == null)
        //    S = this;
        //else 
        //    Debug.LogError("Ошибка Hero.cs!");
    }

    public void GetDamage(float damage)
    {
        GameSettings.playerHP -= damage;
    }
    private void Update()
    {
        if (GameSettings.playerHP <= 0)
        {
            GameOver();
        }
        UpdateHp();
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");
        if (_yAxis < 0) _yAxis = -0.5f;
        _userInput = new Vector2(_xAxis, _yAxis);
        _raftRotation = Quaternion.Euler(-_yAxis * 2f, _xAxis * _raftRotateBorder, _xAxis * _raftTilt);
        if (!GameSettings.gameOnPause)
        {
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
                raft.transform.rotation = Quaternion.Lerp(raft.transform.rotation, _raftRotation, _raftRotationSpeed * Time.deltaTime);
            } }
        else
        {
            raft.transform.rotation = Quaternion.Lerp(raft.transform.rotation, Quaternion.Euler(0f, 0f, 0f), _raftRotationSpeed * Time.deltaTime);
            raftDirection = (_windDirection + _riverDirection) * Time.deltaTime;
        }
        this.gameObject.transform.Translate(new Vector3(raftDirection.x, 0f, raftDirection.y), Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                {
                    if (collision.gameObject.GetComponent<Obstacle>() != null)
                    {
                        collision.gameObject.GetComponent<Obstacle>().Hit(raftDamage);
                    }
                    Vector3 hitPoint = collision.collider.ClosestPoint(gameObject.transform.position);
                    Vector3 backPush = transform.position - hitPoint;
                    backPush.y = 0;
                    gameObject.transform.Translate(backPush, Space.World);
                    break;
                }
            case null:
                {
                    break;
                }
        }
    }
    public void UpdateHp()
    {
        float count;
        count = GameSettings.playerHP / 100;
        if (count * 8 < 1)
        {
            image.sprite = sprites[0];
        }
        else if (count * 8 < 2)
        {
            image.sprite = sprites[1];
        }
        else if (count * 8 < 3)
        {
            image.sprite = sprites[2];
        }
        else if (count * 8 < 4)
        {
            image.sprite = sprites[3];
        }
        else if (count * 8 < 5)
        {
            image.sprite = sprites[4];
        }
        else if (count * 8 < 6)
        {
            image.sprite = sprites[5];
        }
        else if (count * 8 < 7)
        {
            image.sprite = sprites[6];
        }
        else if (count * 8 < 8)
        {
            image.sprite = sprites[7];
        }
    }
    void GameOver()
    {
        GameSettings.gameOnPause = true;

    }
}
