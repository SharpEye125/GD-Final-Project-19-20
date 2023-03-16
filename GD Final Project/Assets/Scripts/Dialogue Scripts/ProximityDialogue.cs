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
    public bool conversing;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        if (distance.magnitude >= talkRange && conversing == true)
        {
            conversing = false;
            timer = 0;
            hasTalked = false;
            FindObjectOfType<DialogueManager>().EndDialogue();
            Debug.Log("Walked away from " + gameObject.name);
        }
        if (hasTalked == false)
        {
            if (distance.magnitude <= talkRange && Input.GetKeyDown(KeyCode.E))
            {
                if (GetComponent<DialogueTrigger>() != null)
                {
                    Debug.Log("Using normal DT");
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    Debug.Log("Using Conditional DT");
                    GetComponent<DialogueTriggerConditional>().TriggerDialogue();
                }
                hasTalked = true;
                conversing = true;
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
