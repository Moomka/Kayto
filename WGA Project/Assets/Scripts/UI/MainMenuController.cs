using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _buttonGame;
    [SerializeField] private Button _buttonExit;

    private void Awake()
    {
        _buttonExit.onClick.AddListener(ExitGame);
        _buttonGame.onClick.AddListener(EnterMainMenu);
    }

    private void EnterMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void ExitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
