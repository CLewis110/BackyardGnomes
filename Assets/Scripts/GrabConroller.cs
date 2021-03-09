using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabConroller : MonoBehaviour
{
    public CharacterController2D cc;
    public Transform grabDetect;
    public Transform boxHolder;
    public float rayDist;
    public bool holding = false;
    public float throwForce = 2.5f;
    private int direction = 0;

    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }
    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (cc.isFacingRight)
            direction = 1;
        else
            direction = -1;

        if(grabCheck.collider != null && grabCheck.collider.tag == "Moveable")
        {          
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (!holding)
                {
                    holding = true;
                    grabCheck.collider.gameObject.transform.parent = boxHolder;
                    grabCheck.collider.gameObject.transform.position = boxHolder.position;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            }

            if(Input.GetKeyUp(KeyCode.G))
            {
                holding = false;
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                

            }

            if(Input.GetKey(KeyCode.F))
            {
                holding = false;
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * direction, 1) * throwForce;
            }
            

        }
    } 
}
