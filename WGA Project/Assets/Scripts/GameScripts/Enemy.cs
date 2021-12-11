using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float radiusView = 5f;
    [SerializeField] private float distanseStartMove = 20f;
    [SerializeField] private float HP = 100;
    private bool _damageDone;
    private GameObject Player;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    void Awake()
    {
        _damageDone = false;
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0) Death();
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = Player.transform.position - transform.position;
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
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                rb.isKinematic = true;
                transform.SetParent(other.gameObject.transform);
                if (_damageDone == false)
                {
                    PlayerController.S.GetDamage(damage);
                    _damageDone = true;
                }
                break;
        }
       /* if (other.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true;
            transform.SetParent(other.gameObject.transform);
            if (_damageDone == false)
            {
                PlayerController.S.GetDamage(damage);
                _damageDone = true;
            }
        }*/
    }
}
