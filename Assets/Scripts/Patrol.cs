using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;

    public bool isInRange = false;
    private bool movingRight = true;

    public GameObject player;

    public Transform groundDetect;


    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, distance);
        if(groundInfo.collider == false)
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

        if(isInRange && player.GetComponent<CharacterController2D>().isRunning)
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {        
            isInRange = true;
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
            isInRange = false;
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
