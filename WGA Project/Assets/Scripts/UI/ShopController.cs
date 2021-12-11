using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Button _buttonHealFrog;
    [SerializeField] private Button _buttonHealRaft;
    [SerializeField] private int _healthGain;

    private void Awake()
    {
        _buttonHealFrog.onClick.AddListener(HealFrog);
        _buttonHealRaft.onClick.AddListener(HealRaft);
    }

    private void HealRaft()
    {
        GameSettings.healtRaft += _healthGain;
        Debug.Log(GameSettings.healtRaft);
    }

    private void HealFrog()
    {
        GameSettings.healthFrog += _healthGain;
        Debug.Log(GameSettings.healthFrog);
    }
}
