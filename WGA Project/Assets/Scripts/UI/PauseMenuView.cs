using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBackToGame;
        [SerializeField] private Button _buttonExit;


        public void Init(UnityAction quitGame, UnityAction backToGame)
        {
            _buttonExit.onClick.AddListener(quitGame);
            _buttonBackToGame.onClick.AddListener(backToGame);
        }

        public void OnDestroy()
        {
            _buttonBackToGame.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }
    }

}