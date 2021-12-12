using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    LineRenderer tongueLine;
    float currentPower;
    Vector3 frogPosition;
    Vector3 maxPowerPoint;
    Vector3 attackVector;

    private void Awake()
    {
        tongueLine = gameObject.GetComponent<LineRenderer>();
        tongueLine.positionCount = 0;
    }
    private void Update()
    {
        frogPosition = frogMouth.position;
        maxPowerPoint = frogPosition + (Vector3.Normalize(GameSettings.mousePosition - frogPosition) * maxPower);
        if (Input.GetMouseButtonDown(0) && GameSettings.playerState == GameSettings.playerStates.raftControl)
        {
            currentPower = 0;
            GameSettings.playerState = GameSettings.playerStates.charging;
        }
        if (Input.GetMouseButtonUp(0) && GameSettings.playerState == GameSettings.playerStates.charging)
        {
            GameSettings.playerState = GameSettings.playerStates.attack;
        }

        switch (GameSettings.playerState)
        {
            case GameSettings.playerStates.attack:
                tongueLine.material = tongueMaterial;
                Attack(frogPosition + attackVector);
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
                    gameObject.transform.position = frogPosition;
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
        if (Vector3.Distance(frogPosition, gameObject.transform.position) <= tongueMinimalDistance)
        {
            gameObject.transform.position = frogPosition;
            GameSettings.playerState = GameSettings.playerStates.raftControl;
        }
        gameObject.transform.LookAt(frogPosition);
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * tongueSpeed);
        DrawTongue();
    }
    void Charging()
    {
        currentPower = Mathf.Clamp(currentPower + (powerGrowSpeed * Time.deltaTime), 0, maxPower);
        attackVector = GameSettings.mousePosition - frogPosition;
        DrawAim();
    }

    void Attack(Vector3 target)
    {
        Debug.Log(currentPower);
        if (Vector3.Distance(gameObject.transform.position, frogPosition) >= currentPower ||
            Vector3.Distance(gameObject.transform.position, frogPosition) >= attackVector.magnitude)
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
        tongueLine.SetPosition(0, frogPosition);
        tongueLine.SetPosition(1, gameObject.transform.position);
    }

    void DrawAim()
    {
        tongueLine.positionCount = 2;
        tongueLine.SetPosition(0, frogPosition);
        tongueLine.SetPosition(1, Vector3.Lerp(frogPosition, maxPowerPoint, currentPower/maxPower));
    }
}
