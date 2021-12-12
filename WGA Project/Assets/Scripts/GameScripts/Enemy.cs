using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float distanseStartMove = 20f;
    [SerializeField] private float HP = 100;
    [SerializeField] private float speed = 5f;

    [SerializeField] public float damage = 10f;

    bool seePlayer;

    public bool WaitOrAttack()
    {
        float distanceToPlayer = (GameSettings.frogPosition - gameObject.transform.position).magnitude;
        if (distanceToPlayer < distanseStartMove)
        {
            FollowPlayer();
            return true;
        }
        return false;
    }

    public void FollowPlayer()
    {
        gameObject.transform.LookAt(GameSettings.frogPosition);
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0) Death(0f);
    }

    private void Death(float t)
    {
        Destroy(this.gameObject, t);
    }   
}
