using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlant : MonoBehaviour
{
    public GameObject flower;
    public GameMaster gm;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.tag == "Garden")
        {
            gm.AddPoints(20);
        }
        if(collision.gameObject.tag == "Plantable")
        {
            
            Instantiate(flower, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

    }
}
