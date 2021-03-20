using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [System.Serializable] 
    
    public class PlayerStats { 
        public float Health = 3f;
    }

    public PlayerStats playerStats = new PlayerStats();
    

    public int fallBoundary = -40;

    public int numOfHearts;
    public Image[] Hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    public CharacterController2D cc;

    public GameObject healthHearts;

    void Awake()
    {
        playerStats.Health = 3f;
    }
    void Update()
    {
        HeartCheck();
    }

    public void DamagePlayer(float damage)
    {
        playerStats.Health -= damage;
        if(playerStats.Health <= 0)
        {
            GameMaster.KillPlayer(this);
            healthHearts.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lawn Mower")
        {
            if (cc.isHiding == false)
                DamagePlayer(1);
        }

    }

    public void HeartCheck()
    {
        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer(Mathf.Infinity);
        }

        if (playerStats.Health > numOfHearts)
        {
            playerStats.Health = numOfHearts;
        }

        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < playerStats.Health)
            {
                Hearts[i].sprite = fullHearts;
            }
            else
            {
                Hearts[i].sprite = emptyHearts;
            }

            if (i < numOfHearts)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }

}
