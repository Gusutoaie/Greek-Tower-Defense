using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScript : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        SceneManager.LoadScene("LevelsMenuScene");
    }

    public void OnOptionsButtonClicked()
    {
        Debug.Log("Options button clicked");
        // Application.LoadLevel("HelpScene");
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("Exit button clicked");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
