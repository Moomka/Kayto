using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float obstacleHP;
    [SerializeField] float [] obstacleDamage = new float [2];
    private bool beenHitted = false;

    public void Hit(float damage)
    {
        GameSettings.playerHP -= beenHitted ? obstacleDamage[1] : obstacleDamage[0];
        
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
