using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Gold { get; private set; } = 10000; // Initial gold
    public List<GameObject> enemyesAlive = new List<GameObject>();




    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
        }
    }

    public bool SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            return true;
        }
        return false;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }
    public void RemoveGold(int amount)
    {

        Gold -= amount;

    }
    public void RestartGame()
    {
        // Reset the singleton instance
        Instance = null;

        // Destroy the GameManager object
        Destroy(gameObject);

        // Reload the first scene (assuming the first scene is at index 0)
        SceneManager.LoadScene("Level1Scene");
        Time.timeScale = 1;
    }

}
