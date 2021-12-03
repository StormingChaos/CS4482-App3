using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //resume game after pause
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GamePaused = false;
    }

    //pause game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GamePaused = true;
    }

    public void RestartGame()
    {
        Resume();
        GameStateManager.victory = false;
        GameStateManager.gameOver = false;
        SceneManager.LoadScene("Level1");
    }

    public void Leaderboard()
    {
        Resume();
        GameStateManager.victory = false;
        GameStateManager.gameOver = false;
        SceneManager.LoadScene("Leaderboard");
    }

    public void MainMenu()
    {
        Resume();
        GameStateManager.victory = false;
        GameStateManager.gameOver = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
