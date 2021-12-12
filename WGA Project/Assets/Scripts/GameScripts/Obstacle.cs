using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float obstacleHP;
    [SerializeField] float obstacleDamage;

    public void Hit(float damage)
    {
        GameSettings.playerHP -= obstacleDamage;
        obstacleHP -= damage;
        if (obstacleHP <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
