using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonExit;


        public void Init(UnityAction quitGame, UnityAction restartGame)
        {
            _buttonExit.onClick.AddListener(quitGame);
            _buttonRestart.onClick.AddListener(restartGame);
        }

        public void OnDestroy()
        {
            _buttonRestart.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }
    }

}