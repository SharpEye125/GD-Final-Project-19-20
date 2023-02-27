using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBroomPickup : MonoBehaviour
{
    public float pickupRange;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (distance.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && player.gameObject.GetComponent<PlatformerMove>().grounded == true)
        {
            //Debug.Log(distance.magnitude);
            if (player.GetComponent<SlimeCleanupTask>().hasMop == true)
            {
                player.GetComponent<SlimeCleanupTask>().hasMop = false;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                player.GetComponent<SlimeCleanupTask>().hasMop = true;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            
        }
    }
}
