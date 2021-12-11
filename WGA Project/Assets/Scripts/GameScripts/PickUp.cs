using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float HP = 100;

    public void GetHit()
    {
        GameSettings.playerScore++;
        Destroy(this.gameObject);
    }
}
