using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    //Movement
    private float moveSpeed = 15;
    public float jumpForce = 5f;
    public bool isFacingRight = true;
    public bool isRunning = false;

    //Hiding
    public bool isHiding = false;
    
    public float throwForce = 3f;
    public int direction = 0;

    //Ground Check
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Rigidbody2D rb;

    //Seed prefab
    public GameObject seed;
    public Transform dropPoint;

    //Bread prefab
    public GameObject bread;

    public Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isFacingRight)
            direction = 1;
        else
            direction = -1;

        //Read movement
        var movement = Input.GetAxis("Horizontal");

        if (movement == 0)
            isRunning = false;

        if(movement != 0)
        {
            isRunning = true;
            if(movement > 0)
            {
                isFacingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
                //transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

            if (movement < 0)
            {
                isFacingRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
                //transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

        }

        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * moveSpeed;

        if(Input.GetKeyDown(KeyCode.R))
        {
            DropSeed();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ThrowBread();
        }

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();
    }

    void ThrowBread()
    {
        GameObject breadToThrow = Instantiate(bread, transform.position, Quaternion.identity);
        breadToThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * direction, 1) * throwForce;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    void DropSeed()
    {
        if(player.seeds > 0)
        {
            Instantiate(seed, dropPoint.transform.position, dropPoint.transform.rotation);
            player.SubtractSeed();
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hideable" || collision.gameObject.tag == "Flower")
        {
            isHiding = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hideable" || collision.gameObject.tag == "Flower")
        {
            isHiding = false;
        }
    }
}
