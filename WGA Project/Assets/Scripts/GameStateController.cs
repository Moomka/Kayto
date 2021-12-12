using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{

    public int sceneCount;
    int curSceneIndex = 0;
    public GameObject[] Buttons = new GameObject[3];


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameSettings.gameOnPause == false)
        {
            pauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GameSettings.gameOnPause == true)
        {
            UnpauseGame();
        }
    }
    public void Restart()
    {
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex);
    }

    public void StartGame()
    {
        curSceneIndex = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextScene()
    {
        curSceneIndex++;
        if (curSceneIndex >= sceneCount) curSceneIndex = 0;
        SceneManager.LoadScene(curSceneIndex);
    }

    public void pauseGame()
    {
        foreach (GameObject button in Buttons)
        {
            button.active = true;
            GameSettings.gameOnPause = true;
        }
    }

    public void UnpauseGame()
    {
        foreach (GameObject button in Buttons)
        {
            button.active = false;
            GameSettings.gameOnPause = true;
        }
    }
}
