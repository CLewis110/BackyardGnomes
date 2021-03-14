using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;

    public bool isVisible = false;
    private bool movingRight = true;

    public GameObject player;

    public Transform groundDetect;



    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Change to turn at fences instead of gaps 
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.right, distance);
        if(groundInfo.collider.tag.Equals("Fence"))
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        //Add check for if !hiding
        if(isVisible && player.GetComponent<CharacterController2D>().isRunning)
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {        
            isVisible = true;
            player = collision.gameObject;
            if (collision.GetComponent<CharacterController2D>().isRunning)
            {
                StopAllCoroutines();
                StartCoroutine(Attack());
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {        
            isVisible = false;
            StopAllCoroutines();
            StartCoroutine(CalmDown());
        }
    }

    IEnumerator Attack()
    {
        speed = 30f;
        yield return null;
    }

    IEnumerator CalmDown()
    {
        yield return new WaitForSeconds(5f);
        speed = 10f;
    }
}
