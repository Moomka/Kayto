using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private int _amountOfPollen;
    public void GetHit()
    {
        GameSettings.playerScore++;
        Destroy(this.gameObject);
        AddPollen();
    }

    public void AddPollen()
    {
        GameSettings.pollen += _amountOfPollen;
    }
}
