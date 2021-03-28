using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 0f;

    public GameObject healthHearts;

    public GameObject deathScreen;

    [SerializeField] TextMeshProUGUI ScoreText;
    public int score;

    void Start()
    {
        ScoreText.text = "Score: 0";
        if(gm == null)
        {
            gm = this;
        }
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        /*
        healthHearts.SetActive(false);
        deathScreen.SetActive(true);


        deathScreen.SetActive(false);
        healthHearts.SetActive(true);        
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        */

        gm.StartCoroutine(gm.RestartLevel());
    }

    public static void KillPlayer(Player player)
    {
        gm.StartCoroutine(gm.RestartLevel());
        /*
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
        */
    }


    //Keep track of score
    public void AddPoints(int points)
    {
        Debug.Log("Point!");
        score += points;
        ScoreText.text = "Score:    " + score.ToString();
    }


    //Restart level
    public IEnumerator RestartLevel()
    {
        healthHearts.SetActive(false);
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
