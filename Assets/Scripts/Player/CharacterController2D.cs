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
    
    //Ground Check
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Rigidbody2D rb;

    //Seed prefab
    public GameObject seed;
    public Transform dropPoint;

    public Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


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

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();



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
