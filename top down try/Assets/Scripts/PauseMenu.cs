using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    public GameObject Player;
    public PlayerBehaviour playerBehaviour;

    public GameObject SettingsMenuObject;
   
    // Start is called before the first frame update


   
        void Awake()
        {
            Player = GameObject.FindWithTag("Player");
           playerBehaviour = Player.GetComponent<PlayerBehaviour>();
        }
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
  
            if (!isPaused)
            {
                PauseGame();

            } 
            else
            {
                ResumeGame();
            }
        }
    }


    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        SettingsMenuObject.SetActive(false);
        Debug.Log("ES IST AKTIV: " + SettingsMenuObject.activeSelf);

    }

    public void ResumeGame()
    {
        if (SettingsMenuObject.activeSelf)
        {
            SettingsMenuObject.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }


    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
