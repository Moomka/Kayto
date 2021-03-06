using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tongue : MonoBehaviour
{
    [SerializeField] Material tongueMaterial;
    [SerializeField] Material aimMaterial;
    [SerializeField] Transform frogMouth;
    [SerializeField] float maxPower;
    [SerializeField] float powerGrowSpeed;
    [SerializeField] float tongueDamage;
    [SerializeField] float tongueSpeed;
    [SerializeField] float tongueMinimalDistance;
    [SerializeField] private GameObject _pollenText;

    LineRenderer tongueLine;
    float currentPower;

    Vector3 maxPowerPoint;
    Vector3 attackVector;

    private void Awake()
    {
        tongueLine = gameObject.GetComponent<LineRenderer>();
        tongueLine.positionCount = 0;
    }
    private void Update()
    {
        GameSettings.frogPosition = frogMouth.position;
        maxPowerPoint = GameSettings.frogPosition + (Vector3.Normalize(GameSettings.mousePosition - GameSettings.frogPosition) * maxPower);
        if (Input.GetMouseButtonDown(0) && GameSettings.playerState == GameSettings.playerStates.raftControl && !GameSettings.gameOnPause)
        {
            currentPower = 0;
            GameSettings.playerState = GameSettings.playerStates.charging;
        }
        if (Input.GetMouseButtonUp(0) && GameSettings.playerState == GameSettings.playerStates.charging && !GameSettings.gameOnPause)
        {
            GameSettings.playerState = GameSettings.playerStates.attack;
        }

        switch (GameSettings.playerState)
        {
            case GameSettings.playerStates.attack:
                tongueLine.material = tongueMaterial;
                Attack(GameSettings.frogPosition + attackVector);
                break;
            case GameSettings.playerStates.charging:
                tongueLine.material = aimMaterial;
                Charging();
                break;
            case GameSettings.playerStates.raftControl:
                tongueLine.positionCount = 0;
                break;
            case GameSettings.playerStates.tongueReturn:
                TongueReturn();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                {
                    GameSettings.playerState = GameSettings.playerStates.raftControl;
                    gameObject.transform.position = GameSettings.frogPosition;
                    break;
                }
            case "Enemy":
                {
                    other.gameObject.GetComponent<Enemy>().GetDamage(tongueDamage);
                    break;
                }
            case "PickUp":
                {
                    other.gameObject.GetComponent<PickUp>().GetHit();
                    _pollenText.GetComponent<Text>().text = "Amount of pollen: " + GameSettings.pollen.ToString();
                    break;
                }
            case "Obstacle":
                {
                    GameSettings.playerState = GameSettings.playerStates.tongueReturn;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(maxPowerPoint, 0.5f);
    }

    void TongueReturn()
    {
        if (Vector3.Distance(GameSettings.frogPosition, gameObject.transform.position) <= tongueMinimalDistance)
        {
            gameObject.transform.position = GameSettings.frogPosition;
            GameSettings.playerState = GameSettings.playerStates.raftControl;
        }
        gameObject.transform.LookAt(GameSettings.frogPosition);
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * tongueSpeed);
        DrawTongue();
    }
    void Charging()
    {
        currentPower = Mathf.Clamp(currentPower + (powerGrowSpeed * Time.deltaTime), 0, maxPower);
        attackVector = GameSettings.mousePosition - GameSettings.frogPosition;
        DrawAim();
    }

    void Attack(Vector3 target)
    {
        if (Vector3.Distance(gameObject.transform.position, GameSettings.frogPosition) >= currentPower ||
            Vector3.Distance(gameObject.transform.position, GameSettings.frogPosition) >= attackVector.magnitude)
        {
            GameSettings.playerState = GameSettings.playerStates.tongueReturn;
        }
        gameObject.transform.LookAt(target);
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * tongueSpeed);
        DrawTongue();
    }

    void DrawTongue()
    {
        tongueLine.positionCount = 2;
        tongueLine.SetPosition(0, GameSettings.frogPosition);
        tongueLine.SetPosition(1, gameObject.transform.position);
    }

    void DrawAim()
    {
        tongueLine.positionCount = 2;
        tongueLine.SetPosition(0, GameSettings.frogPosition);
        tongueLine.SetPosition(1, Vector3.Lerp(GameSettings.frogPosition, maxPowerPoint, currentPower/maxPower));
    }
}
