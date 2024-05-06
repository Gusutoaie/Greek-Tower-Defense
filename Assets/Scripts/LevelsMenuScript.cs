using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenuScript : MonoBehaviour
{
    public Canvas canvas;
    private readonly List<Button> _levelButtons = new List<Button>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in canvas.transform)
        {
            var button = child.GetComponent<Button>();
            if (button != null)
            {
                _levelButtons.Add(button);
            }
        }

        foreach (var button in _levelButtons)
        {
            button.onClick.AddListener(() =>
            {
                // Debug.Log(button.name + " clicked");
                if (button.name == "BackButton")
                {
                    SceneManager.LoadScene("MainMenuScene");
                }
                else
                {
                    SceneManager.LoadScene(button.name.Substring(0, button.name.Length - 6) + "Scene");
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
