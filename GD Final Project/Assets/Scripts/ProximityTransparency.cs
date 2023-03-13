using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTransparency : MonoBehaviour
{
    Transform player;
    public float minDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlatformerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (distance.magnitude <= minDistance)
        {
            //maybe use different colors to fade between colors
        }
    }
}
