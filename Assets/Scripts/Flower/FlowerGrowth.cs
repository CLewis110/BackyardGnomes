using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGrowth : MonoBehaviour
{
    private float timer = 0f;
    private float growTime = 1.5f;
    private float maxSize = 3f;
    public float flowerLife = 2f;

    //height
    public float maxHeight;

    private bool isMaxSize = false;


    void Start()
    {
        maxHeight = transform.position.y + 4f;
        if (isMaxSize == false)
        {
            StartCoroutine(Grow());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lawn Mower")
        {
            //Debug.Log("Ouch!");
            FlowerHit();
        }

    }
    private IEnumerator Grow()
    {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2( maxSize, maxSize);

        //height
        Vector2 startHeight = transform.localPosition;
        Vector2 finalHeight = new Vector2(transform.localPosition.x, maxHeight);


        do
        {
            transform.localPosition = Vector3.Lerp(startHeight, finalHeight, timer / growTime);
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer / growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < growTime);

        isMaxSize = true;
    } 

    public void FlowerDeath()
    {
        Destroy(this.gameObject);
    }

    public void FlowerHit()
    {
        if (flowerLife > 0)
        {
            flowerLife -= 1;
            if (flowerLife <= 0)
                FlowerDeath();
        }
        else
            FlowerDeath();
    }
}
