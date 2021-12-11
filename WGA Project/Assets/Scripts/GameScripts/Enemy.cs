using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float HP = 100;

    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0) Death();
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
