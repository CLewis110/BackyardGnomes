using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGrowth : MonoBehaviour
{
    private float timer = 0f;
    private float growTime = 6f;
    private float maxSize = 1f;

    //height
    public float maxHeight = -0.5f;

    private bool isMaxSize = false;

    void Start()
    {
        if (isMaxSize == false)
        {
            StartCoroutine(Grow());
        }
    }

    private IEnumerator Grow()
    {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2(maxSize, maxSize);

        //height
        Vector2 startHeight = transform.position;
        Vector2 finalHeight = new Vector2(transform.position.x, maxHeight);


        do
        {
            transform.position = Vector3.Lerp(startHeight, finalHeight, timer / growTime);
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer / growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < growTime);

        isMaxSize = true;
    } 
}
