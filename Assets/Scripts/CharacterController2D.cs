using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    //Movement
    public float moveSpeed = 15;
    public float jumpForce = 5;
    public bool isFacingRight = true;
    public bool isRunning = false;

    
    //Ground Check
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        //Escape program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Toggle run on and off
        /*if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = false;
        if(Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = true;

        if (isRunning)
            moveSpeed = 15;
        else 
            moveSpeed = 5;
        */

        //Read movement
        var movement = Input.GetAxis("Horizontal");

        if (movement == 0)
            isRunning = false;

        if(movement != 0)
        {
            isRunning = true;
            if(movement > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            //transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            if (movement < 0)
                transform.eulerAngles = new Vector3(0, -180, 0);
            //transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * moveSpeed;


        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();


    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }


}
