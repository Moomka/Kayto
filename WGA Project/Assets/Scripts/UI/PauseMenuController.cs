using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui 
{
    public class PauseMenuController : MonoBehaviour
    {
        private string _resourcePath ="Prefabs/PauseMenu12";
        private PauseMenuView _view;
        public Transform placeForUi;
        private GameObject objectView;
        [SerializeField] private Button _buttonHealFrog;
        [SerializeField] private Button _buttonHealRaft;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)&& GameSettings.gameOnPause == false)
            {
                _view = LoadView(placeForUi);
                _view.Init(QuitGame, BackTogame, BackToMenu);
                _buttonHealFrog.gameObject.SetActive(false);
                _buttonHealRaft.gameObject.SetActive(false);
            }

        }

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab= (GameObject)Resources.Load(_resourcePath);
            objectView = Object.Instantiate(prefab, placeForUi, false);
            GameSettings.gameOnPause = true;
            Debug.Log("загрузили");
            return objectView.GetComponent<PauseMenuView>();
        }

        private void QuitGame() =>
            Application.Quit();

        private void BackTogame()
        {
            GameSettings.gameOnPause = false;
            Debug.Log("Вернулись");
            Destroy(objectView);
            _buttonHealFrog.gameObject.SetActive(true);
            _buttonHealRaft.gameObject.SetActive(true);
        }
        private void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}