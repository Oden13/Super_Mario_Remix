using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void SwitchScene(string Game)
{
        SceneManager.LoadScene("Game");
    }
    public void quitgame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

     public void LoadMainMenu(string MainMenu)
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
