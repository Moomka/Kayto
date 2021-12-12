using System.Collections;
using System.Collections.Generic;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController S;
    public Vector2 raftDirection;

    [SerializeField] Vector2 _riverDirection = new Vector2(0, 1.2f);
    [SerializeField] private float _raftRotationSpeed;
    [SerializeField] private float _raftRotateBorder;
    [SerializeField] private float _raftTilt;
    [SerializeField] GameObject raft;
    [SerializeField] private Transform placeForUi;
    [SerializeField] float raftDamage;

    private Quaternion _raftRotation;
    private Vector2 _userInput;
    private Vector2 _windDirection;
    private GameSettings.playerStates _playerState;
    private float _xAxis;
    private float _yAxis;
    GameObject objectView;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (S == null)
            S = this;
        else 
            Debug.LogError("Ошибка Hero.cs!");
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

        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");
        if (_yAxis < 0) _yAxis = 0;
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

    void GameOver()
    {
    GameOverView _view;
    _view = LoadView(placeForUi);
    _view.Init(QuitGame, RestartGame);
    }

    private GameOverView LoadView(Transform placeForUi)
    {
        string _resourcePath = "Prefabs/GameOverMenu";
        GameObject prefab = (GameObject)Resources.Load(_resourcePath);
        objectView = Object.Instantiate(prefab, placeForUi, false);
        GameSettings.gameOnPause = true;
        return objectView.GetComponent<GameOverView>();
    }

    private void QuitGame() =>
        Application.Quit();

    private void RestartGame()
    {
        GameSettings.gameOnPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(objectView);
    }
}
