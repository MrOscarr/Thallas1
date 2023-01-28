using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   

    public static bool isPaused = false;
    public GameObject pauseMenu;
    private PlayerDeath playerDeath;
    
    void Awake()
    {
        playerDeath = GameObject.Find("player").GetComponent<PlayerDeath>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                GameObject.Find("player").GetComponent<New_Playermovement>().enabled = true; 
                ResumeGame();
            }
            else
            {
                GameObject.Find("player").GetComponent<New_Playermovement>().enabled = false;
                PauseGame();
                
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    { 
        if(playerDeath.IsRespawn == true)
        {
            GameObject.Find("player").GetComponent<New_Playermovement>().enabled = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        if(playerDeath.IsRespawn == false)
        {
            GameObject.Find("player").GetComponent<New_Playermovement>().enabled = true; 
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;   
        }
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
