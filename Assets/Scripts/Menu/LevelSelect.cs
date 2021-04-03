using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(3);
    }
}
