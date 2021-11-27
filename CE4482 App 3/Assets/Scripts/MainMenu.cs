using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Call when Start Button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // Call when Leaderboard Button is pressed
    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    // Call when Quit Button is pressed
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
