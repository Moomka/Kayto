using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField] float _maxAttackDistance;
    [SerializeField] float tongueSpeed;
    float attackDistance;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameSettings.playerState != GameSettings.playerStates.attack)
        {
            gameObject.transform.LookAt(GameSettings.mousePosition);
            GameSettings.playerState = GameSettings.playerStates.attack;
            attackDistance = Vector3.Distance(gameObject.transform.position, GameSettings.mousePosition);
        }

        if (GameSettings.playerState == GameSettings.playerStates.attack)
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * tongueSpeed);
            float distance = Vector3.Distance(gameObject.transform.parent.transform.position, gameObject.transform.position);
            if (distance > attackDistance || distance > _maxAttackDistance)
            {
                gameObject.transform.LookAt(gameObject.transform.parent.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSettings.playerState = GameSettings.playerStates.raftControl;
            gameObject.transform.position = gameObject.transform.parent.transform.position;
        }
    }
}
