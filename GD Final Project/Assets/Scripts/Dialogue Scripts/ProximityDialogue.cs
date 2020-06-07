using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDialogue : MonoBehaviour
{
    public float talkRange = 5f;
    public Transform player;
    public bool hasTalked = false;
    float timer = 0;
    public float waitTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (hasTalked == false)
        {
            if (distance.magnitude <= talkRange && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<DialogueTrigger>().TriggerDialogue();
                hasTalked = true;
            }
        }
        if (hasTalked == true)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                timer = 0;
                hasTalked = false;
            }
        }
    }
}
