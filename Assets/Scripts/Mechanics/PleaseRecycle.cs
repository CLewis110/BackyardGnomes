using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PleaseRecycle : MonoBehaviour
{
    public GameMaster gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameMaster>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Recyclable")
        {        
            //Add points
            gm.AddPoints(20);
            gm.AddRecyclable();
            Recycle();
        }
    }
    void Recycle()
    {
        Destroy(this.gameObject);
    }
}
