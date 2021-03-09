using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public CharacterController2D cc;
    public GameObject player;
    public float offset = 4;
    public float smoothMove = 15f;
    Vector3 velocity = Vector3.zero;

    private float yPosClamp = -1;
    private float timeToSearch = 0;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        cc = player.GetComponent<CharacterController2D>();
    }
    private void LateUpdate()
    {
        if (target == null)
            FindPlayer();
        Follow();   
    }
   
    void Follow()
    {
        Vector3 temp = transform.position;
        temp.x = target.position.x;
        temp.y = target.position.y;
        if(cc.isFacingRight)
            temp.x += offset;
        if (!cc.isFacingRight)
            temp.x -= offset;

        //clamp camera
        temp = new Vector3(temp.x, Mathf.Clamp(temp.y, yPosClamp, Mathf.Infinity), temp.z);

        transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothMove * Time.fixedDeltaTime);
    }

    void FindPlayer()
    {
        if(timeToSearch <= Time.time)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                target = player.transform;
            }
            timeToSearch = Time.time + 0.5f;
        }
    }            
}
