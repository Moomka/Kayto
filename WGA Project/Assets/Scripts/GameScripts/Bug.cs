using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] Transform raftSpot;
    [SerializeField] float damageCooldown;
    float currentTime;
    bool canBite = false;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                gameObject.transform.position = raftSpot.position;
                transform.SetParent(other.gameObject.transform);
                gameObject.transform.LookAt(GameSettings.frogPosition);
                canBite = true;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (canBite)
        {
            Bite();
        }
        else
        {
            gameObject.GetComponent<Enemy>().WaitOrAttack();
        }
    }

    void Bite()
    {
        if (currentTime > damageCooldown)
        {
            currentTime = 0;
            GameSettings.playerHP -= gameObject.GetComponent<Enemy>().damage;
        }
    }
}
