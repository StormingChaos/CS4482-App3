using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public static List<KeyValuePair<string, float>> scores = new List<KeyValuePair<string, float>>();
    internal const string fileName = "leaderboard";

    private void Awake()
    {
        Load();
        Sort();
    }

    private void Start()
    {
        Print();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Print()
    {
        int i = 0;
        textBox.text = "";
        foreach (KeyValuePair<string, float> elem in scores)
        {
            textBox.text += (i + 1) + ". " + elem.Key + " \t\t\t " + elem.Value + "s\n";

            if (++i >= 5)
            {
                break;
            }
        }
    }

    public static void Load()
    {
        scores = Fileio.read(fileName);
    }

    public static void Save()
    {
        Fileio.write(fileName, scores, false);
    }
    public static void AddEntry(string name, float time)
    {
        //space is an allowed character
        scores.Add(new KeyValuePair<string, float>(name, time));
        Sort();
    }

    public static void Sort()
    {
        scores.Sort((left, right) => left.Value.CompareTo(right.Value));
    }
}
