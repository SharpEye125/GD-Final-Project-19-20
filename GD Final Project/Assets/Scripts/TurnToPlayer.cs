using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToPlayer : MonoBehaviour
{
    public Transform player;
    public bool rightFacingSprite;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlatformerMove>().transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x <= transform.position.x)
        {
            if (rightFacingSprite)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            if (rightFacingSprite)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
