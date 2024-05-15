using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Make sure to attach this script to the PlayAgain GameObject
public class PlayAgain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //If the left mouse button is clicked
        {
            GameManager.Instance.RestartGame(); //Reset the towers
        }
    }
}
