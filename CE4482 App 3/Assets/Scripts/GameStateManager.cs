using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static bool victory = false;         // manages if the game has been won by the player
    public static bool gameOver = false;        // manages if the player has died
    public static int kills;                    // manages how many kills the player has
    public int maxKills = 7;                    // how many kills needed to win the game

    public GameObject gameOverUI;               // reference to the GameOver screen
    public GameObject victoryUI;                // reference to the Victory screen
    public TextMeshProUGUI textEntry;           // reference to name entry component
    public static bool getInput = false;        // if input can be received or not

    public static GameObject bat;               // reference to equipped bat
    public static GameObject pistol;            // reference to equipped pistol
    public static GameObject rifle;             // reference to equipped rifle

    private void Start()
    {
        // set up references to equipped weapons
        bat = GameObject.FindGameObjectWithTag("Bat");
        pistol = GameObject.FindGameObjectWithTag("Pistol");
        rifle = GameObject.FindGameObjectWithTag("Rifle");

        // disable the pistol and rifle since the player starts with the bat
        pistol.SetActive(false);
        rifle.SetActive(false);

        // reset kills and victory state when level starts
        kills = 0;
        victory = false;
        gameOver = false;
    }

    private void Update()
    {
        if (kills == maxKills)
        {
            victory = true;
            victoryUI.SetActive(true);

            if (PauseMenu.GamePaused)
            {
                victoryUI.SetActive(false);
            }
            else
            {
                victoryUI.SetActive(true);
            }
        }

        if (gameOver)
        {
            gameOverUI.SetActive(true);

            if (PauseMenu.GamePaused)
            {
                gameOverUI.SetActive(false);
            }
            else
            {
                gameOverUI.SetActive(true);
            }
        }
    }

    public void EnableTextEntry()
    {
        getInput = true;
    }

    public void DisableTextEntry()
    {
        getInput = false;
    }

    public void SubmitScore()
    {
        Leaderboard.AddEntry(textEntry.text, Timer.timer);
        Leaderboard.Save();
        Debug.Log("Submitting Score...");
        victory = false;
        SceneManager.LoadScene("Leaderboard");
    }
}
