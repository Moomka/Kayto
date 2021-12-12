using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Button _buttonHealFrog;
    [SerializeField] private int _healthGain;

    private void Awake()
    {
        _buttonHealFrog.onClick.AddListener(HealFrog);
    }

    private void HealFrog()
    {
        if (GameSettings.pollen>=2) 
        {
            GameSettings.pollen -= 2;
            GameSettings.playerHP += _healthGain;
            Debug.Log(GameSettings.playerHP);
        }
    }
}
