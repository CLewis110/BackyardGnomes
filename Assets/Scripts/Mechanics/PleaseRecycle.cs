using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseRecycle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Recyclable")
        {
            Recycle();
        }
    }
    void Recycle()
    {
        //Add points

        Destroy(this.gameObject);
    }
}
