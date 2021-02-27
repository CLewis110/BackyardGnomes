using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public CharacterController2D cc;
    public float offset = 4;
    public float smoothMove = 15f;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }
    private void LateUpdate()
    {
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
        transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothMove * Time.fixedDeltaTime);
    }
}
