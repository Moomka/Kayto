using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float radiusView = 5f;
    [SerializeField] private float distanseStartMove = 20f;
    [SerializeField] private float HP = 100;
    private bool _damageDone;
    private GameObject Player;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    [SerializeField] public float damage = 10f;
    void Awake()
    {
        _damageDone = false;
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
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

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        Vector3 direction = GameSettings.frogPosition - transform.position;
        float dist = direction.magnitude;
        if (dist < distanseStartMove)
        {
            if (dist > radiusView)
            {
                transform.Translate(-Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                if (rb.isKinematic == false)
                {
                    direction.Normalize();
                    transform.Translate(direction * Time.deltaTime * speed);
                }
            }
        }
    }
    
}
