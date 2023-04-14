using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderTask : MonoBehaviour
{
    public float pickupRange = 2f;
    public float beholderRange = 3f;
    public bool hasLens = false;
    Transform player;
    Transform beholder;
    Vector3 startingPos;
    
    //[SerializeField] float pD;
    //[SerializeField] float bD;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlatformerMove>().transform;
        beholder = FindObjectOfType<BeholderLaser>().transform;
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool pDead = player.GetComponent<PlayerDie>().dead;
        Vector2 playerDistance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        Vector2 beholderDistance = new Vector2(player.position.x - beholder.position.x, player.position.y - beholder.position.y);
        //pD = playerDistance.magnitude;
        //bD = beholderDistance.magnitude;
        if (pDead == true)
        {
            hasLens = false;
            GetComponent<SpriteRenderer>().enabled = true;
            //transform.position = startingPos;
        }
        if (beholderDistance.magnitude <= beholderRange && Input.GetKeyDown(KeyCode.E) && pDead == false)
        {
            if (hasLens == true)
            {
                Debug.Log("Dropping Lens Onto Pupil");
                beholder.GetComponent<BeholderLaser>().laserOn = false;
                hasLens = false;
                GetComponent<SpriteRenderer>().enabled = true;
                transform.position = beholder.position;
            }
            else
            {
                hasLens = true;
                GetComponent<SpriteRenderer>().enabled = false;
                beholder.GetComponent<BeholderLaser>().laserOn = true;
            }
        }
        else if (playerDistance.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && pDead == false)
        {
            if (hasLens == true)
            {
                Debug.Log("Dropping Lens");
                hasLens = false;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Debug.Log("Picking up Lens");
                hasLens = true;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (hasLens)
        {
            transform.position = player.position;
        }

    }
}
