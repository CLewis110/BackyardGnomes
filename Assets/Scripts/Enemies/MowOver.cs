using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowOver : MonoBehaviour
{
    public float mowForce = 10f;

    void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Moveable")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up  * mowForce, ForceMode2D.Impulse);
        }
    }
    //github test
}
