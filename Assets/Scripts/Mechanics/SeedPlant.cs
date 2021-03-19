using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlant : MonoBehaviour
{
    public GameObject flower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Plantable")
        {
            Instantiate(flower, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
