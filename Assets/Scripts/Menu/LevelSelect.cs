using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject backStory;

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        backStory.SetActive(false);
    }

    public void Backstory()
    {        
        backStory.SetActive(true);
        mainMenu.SetActive(false);

    }



}
