using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayerDepth : MonoBehaviour
{
    public float offset = 0f;
    int playerOIL;
    Transform playerPos;
    int defaultOIL;
    // Start is called before the first frame update
    void Start()
    {
        playerOIL = FindObjectOfType<PlatformerMove>().GetComponent<SpriteRenderer>().sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = FindObjectOfType<PlatformerMove>().GetComponent<Transform>();
        if (playerPos.position.y > transform.position.y + offset)
        {
            GetComponent<SpriteRenderer>().sortingOrder = (playerOIL + 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = (playerOIL - 1);
        }
    }
}
