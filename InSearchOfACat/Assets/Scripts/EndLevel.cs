using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("START");
            SceneManager.LoadScene("Intro");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("QUIT");
            Application.Quit();
        } 
    }
}
