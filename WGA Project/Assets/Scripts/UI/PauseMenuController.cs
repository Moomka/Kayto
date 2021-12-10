using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ui 
{
    public class PauseMenuController : MonoBehaviour
    {
        private string _resourcePath ="Prefabs/PauseMenu12";
        private PauseMenuView _view;
        public Transform placeForUi;
        private GameObject objectView;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)&& GameSettings.gameOnPause == false)
            {
                _view = LoadView(placeForUi);
                _view.Init(QuitGame, BackTogame);
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
        }

    }
}