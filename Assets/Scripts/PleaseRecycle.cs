using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseRecycle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Recycle();
    }
    void Recycle()
    {
        //Add points

        Destroy(this.gameObject);
    }
}
