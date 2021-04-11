using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;

    public AudioSource menu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                menu.Play();
                Resume();
                
            } else
            {
                menu.Play();
                Pause ();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Quit()
    {
        Debug.Log("Quitting Game....");
        Application.Quit();
        SceneManager.LoadScene("Title Screen");
    }

    public void Options()
    {
        OptionsMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);

    }

    public void BackToPauseMenu()
    {
        PauseMenuUI.SetActive(true);
        OptionsMenuUI.SetActive(false);
    }
}
