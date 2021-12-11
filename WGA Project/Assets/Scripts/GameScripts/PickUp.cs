using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public void GetHit()
    {
        GameSettings.playerScore++;
        Destroy(this.gameObject);
    }
}
