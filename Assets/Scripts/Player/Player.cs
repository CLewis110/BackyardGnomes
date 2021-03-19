using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats { 
        public float Health = 3f;
    }

    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundary = -40;

    void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(Mathf.Infinity);
        }
    }

    public void DamagePlayer(float damage)
    {
        playerStats.Health -= damage;
        if(playerStats.Health <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

}
