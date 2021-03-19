using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class BirdAI : MonoBehaviour
{
    public Transform target;

    //Times updated per second
    public float updateRate = 2f;

    //Caching
    private Seeker seeker;
    private Rigidbody2D rb;

    //Calculated path
    public Path path;

    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    public float nextWaypointDist = 3f;

    private int currentWaypoint = 0;

    private bool searchingForPlayer = false;
    private bool isInRange = false;
    public bool flowersInRange = false;
    public bool playerInRange = false;

    public GameObject currentFlower;

    public GameObject sResult;

    void Start()
    {
        target = null;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        /*
        if(target == null)
        {
            if(!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        //Creates 
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath()); 
        */
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flower")
        {
            currentFlower = collision.gameObject;
            target = collision.transform;
        }

        if(collision.gameObject.tag == "Player" && currentFlower == null)
        {
            playerInRange = true;
            target = collision.transform;
        }


        /*
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Flower")
        {

            if (target == null)
            {
                if (!searchingForPlayer)
                {
                    searchingForPlayer = true;
                    StartCoroutine(SearchForPlayer());
                }
                return;
            }
        */
            //Creates 
            seeker.StartPath(transform.position, target.position, OnPathComplete);

            StartCoroutine(UpdatePath());
        /*}*/

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flower")
        {
            flowersInRange = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    IEnumerator SearchForPlayer()
    {
        if(playerInRange)
        {
            sResult = GameObject.FindGameObjectWithTag("Player");

            if(sResult == null)
            {
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(SearchForPlayer());
            }
            else
            {
                searchingForPlayer = false;
                target = sResult.transform;
                StartCoroutine(UpdatePath());
                yield return null;
            }
        }


    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return null;
        }


        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("Path found. Any errors? " + p.error);
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {

        if (target == null && isInRange == true)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }
        

        if (path == null)
            return;
        
        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Direction to next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        
        //Face other way
        if(dir.y > 0)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    
        dir *= speed * Time.fixedDeltaTime;

        //Move AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDist)
        {
            currentWaypoint++;
            return;
        }
    }
}
